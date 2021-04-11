using EducationProject.Contract.Common;
using EducationProject.Contract.ResponseModel.Education;
using EducationProject.Contract.ResponseModel.UserEducation;
using EducationProject.Core.Constants;
using EducationProject.Core.Entities;
using EducationProject.Data.DbContexts;
using EducationProject.Service.IServices;
using EducationProject.Service.IUnitOfWorks;
using EducationProject.Service.Service;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProject.Service.Services
{
    public class UserEducationService : BaseService<UserEducation>, IUserEducationService
    {
        public UserEducationService(EducationDbContext db, IUnitOfWork uow) : base(db, uow)
        {

        }
        public List<EducationResponseModel> GetAllEducation()
        {

            var list = _dbContext.Education.Where(x => !x.IsDeleted).Include(x => x.Category).ToList();
            var model = new List<EducationResponseModel>();

            var userEducationList = _dbContext.UserEducation.Where(x => !x.IsDeleted && x.UserEducationStatus.Name == Constants.UserEducationStatus.Request.ToString()).ToList();

            foreach (var item in list)
            {
                if (userEducationList.Where(x => x.EducationId == item.Id).FirstOrDefault() == null)
                {
                    var resModel = item.Adapt<EducationResponseModel>();
                    model.Add(resModel);
                }

            }

            return model;

        }
        public async Task<EducationResponseModel> BuyEducation(ByIdRequestModel model)
        {
            var education = await _dbContext.Education.Where(x => !x.IsDeleted && x.Id == model.Id).FirstOrDefaultAsync();

            if (education != null)
            {
                var userEducation = new UserEducation();
                userEducation.Education = education;
                userEducation.UserId = model.UserId.Value;

                var userEducationStatus = await _dbContext.UserEducationStatus.Where(x => !x.IsDeleted && x.Name == Constants.UserEducationStatus.Request.ToString()).FirstOrDefaultAsync();

                if (userEducationStatus != null)
                {
                    userEducation.UserEducationStatus = userEducationStatus;
                    education.RecordUserId = model.UserId.Value;

                    await _dbContext.UserEducation.AddAsync(userEducation);

                    var resultValue = await _uow.CommitAsync();

                    if (resultValue)
                    {
                        var resModel = education.Adapt<EducationResponseModel>();
                        return resModel;
                    }


                }

            }

            return null;
        }
        public List<UserEducationResponseModel> GetAllEducationByUserId(ByIdRequestModel model)
        {

            var userEducationList = _dbContext.UserEducation.Where(x => !x.IsDeleted && x.UserId == model.Id && x.UserEducationStatus.Name == Constants.UserEducationStatus.Request.ToString() && !x.IsCompleted).Include(x => x.Education).ThenInclude(x => x.Category).ToList();

            var resModel = new List<UserEducationResponseModel>();

            foreach (var item in userEducationList)
            {
                var md = new UserEducationResponseModel();
                md = item.Adapt<UserEducationResponseModel>();
                md.EducationResponseModel = item.Education.Adapt<EducationResponseModel>();

                resModel.Add(md);
            }

            return resModel;

        }
        public List<UserEducationResponseModel> GetAllCompletedEducationByUserId(ByIdRequestModel model)
        {

            var userEducationList = _dbContext.UserEducation.Where(x => !x.IsDeleted && x.UserId == model.Id && x.UserEducationStatus.Name == Constants.UserEducationStatus.Request.ToString() && x.IsCompleted).Include(x => x.Education).ThenInclude(x => x.Category).ToList();

            var resModel = new List<UserEducationResponseModel>();

            foreach (var item in userEducationList)
            {

                var md = new UserEducationResponseModel();
                md = item.Adapt<UserEducationResponseModel>();
                md.EducationResponseModel = item.Education.Adapt<EducationResponseModel>();

                resModel.Add(md);
            }

            return resModel;

        }
        public async Task<EducationContentResponseModel> StartEducationByEducationId(ByIdRequestModel model)
        {

            var userEducation = await _dbContext.UserEducation.Where(x => !x.IsDeleted && x.Id == model.Id).Include
(x => x.Education).ThenInclude(x => x.EducationContentList).FirstOrDefaultAsync();

            if (userEducation != null)
            {
                var content = userEducation.Education.EducationContentList.Where(x => !x.IsDeleted).OrderBy(x => x.RowNumber).FirstOrDefault();

                var contentNext = userEducation.Education.EducationContentList.Where(x => !x.IsDeleted && x.RowNumber > content?.RowNumber).OrderBy(x => x.RowNumber).FirstOrDefault();

                if (content!=null)
                {
                    var educationContent = content.Adapt<EducationContentResponseModel>();

                    educationContent.IsBack = false;
                    if (contentNext != null)
                    {
                        educationContent.IsNext = true;
                    }

                    educationContent.UserEducationId = userEducation.Id;

                    return educationContent;
                }
            }

            return null;

        }
        public async Task<EducationContentResponseModel> ContinueEducationByUserEducationId(ByIdRequestModel model)
        {

            var userEducation = await _dbContext.UserEducation.Where(x => !x.IsDeleted && x.Id == model.Id).Include
(x => x.Education).ThenInclude(x => x.EducationContentList).FirstOrDefaultAsync();

            if (userEducation != null)
            {
                var content = userEducation.Education.EducationContentList.Where(x => !x.IsDeleted && x.RowNumber > userEducation.LastWathedOrderNo).OrderBy(x => x.RowNumber).FirstOrDefault();

                var contentBack = userEducation.Education.EducationContentList.Where(x => !x.IsDeleted && x.RowNumber < content?.RowNumber).OrderBy(x => x.RowNumber).FirstOrDefault();

                var contentNext = userEducation.Education.EducationContentList.Where(x => !x.IsDeleted && x.RowNumber > content.RowNumber).OrderBy(x => x.RowNumber).FirstOrDefault();

                var educationContent = content.Adapt<EducationContentResponseModel>();

                if (contentNext != null)
                {
                    educationContent.IsNext = true;
                }
                if (contentBack != null)
                {

                    educationContent.IsBack = true;
                }
                educationContent.UserEducationId = userEducation.Id;

                return educationContent;

            }



            return null;

        }
        public async Task<EducationContentResponseModel> NextByEducationContentId(Guid Id, Guid UserEducationId)
        {
            var education = await _dbContext.EducationContent.Where(x => !x.IsDeleted && x.Id == Id).Include
            (x => x.Education).FirstOrDefaultAsync();

            if (education != null)
            {
                var content = await _dbContext.EducationContent.Where(x => !x.IsDeleted && x.RowNumber > education.RowNumber).OrderBy(x => x.RowNumber).FirstOrDefaultAsync();

                var contentNext = _dbContext.EducationContent.Where(x => !x.IsDeleted && x.RowNumber > content.RowNumber).OrderBy(x => x.RowNumber).FirstOrDefault();

                var educationContent = content.Adapt<EducationContentResponseModel>();

                educationContent.IsBack = true;
                if (contentNext != null)
                {
                    educationContent.IsNext = true;
                }

                var userEducation = await _dbContext.UserEducation.Where(x => !x.IsDeleted && x.Id == UserEducationId).Include
            (x => x.Education).FirstOrDefaultAsync();

                if (userEducation != null)
                {
                    userEducation.LastWathedOrderNo = education.RowNumber;
                    await _uow.CommitAsync();

                    educationContent.UserEducationId = userEducation.Id;

                }

                return educationContent;

            }

            return null;
        }
        public async Task<EducationContentResponseModel> BackByEducationContentId(Guid Id, Guid UserEducationId)
        {

            var education = await _dbContext.EducationContent.Where(x => !x.IsDeleted && x.Id == Id).Include
            (x => x.Education).FirstOrDefaultAsync();

            var content = await _dbContext.EducationContent.Where(x => !x.IsDeleted && x.RowNumber < education.RowNumber).OrderByDescending(x => x.RowNumber).FirstOrDefaultAsync();

            var contentBack = _dbContext.EducationContent.Where(x => !x.IsDeleted && x.RowNumber < content.RowNumber).OrderByDescending(x => x.RowNumber).FirstOrDefault();

            var educationContent = content.Adapt<EducationContentResponseModel>();

            //Back islemi yapabiliyosa,next yapabilir.
            educationContent.IsNext = true;

            if (contentBack != null)
            {
                educationContent.IsBack = true;
            }

            educationContent.UserEducationId = UserEducationId;


            return educationContent;

        }
        public async Task CompletedEducation(Guid Id)
        {
            var userEducation = await _dbContext.UserEducation.Where(x => !x.IsDeleted && x.Id == Id).Include
            (x => x.Education).FirstOrDefaultAsync();

            userEducation.IsCompleted = true;

            await _uow.CommitAsync();

        }
        public async Task TrainingEducation(ByIdRequestModel model)
        {
            var userEducation = await _dbContext.UserEducation.Where(x => !x.IsDeleted && x.Id == model.Id).Include
            (x => x.Education).FirstOrDefaultAsync();

            userEducation.IsCompleted = false;
            userEducation.LastWathedOrderNo = null;

            await _uow.CommitAsync();

        }
        public async Task<UserEducationResponseModel> CancelUserEducationAsync(Guid UserEducationId)
        {
            var userEducation = await _dbContext.UserEducation.Where(x => x.Id == UserEducationId && !x.IsDeleted).FirstOrDefaultAsync();

            if (userEducation != null)
            {
                userEducation.IsDeleted = true;

                var resultValue = await _uow.CommitAsync();

                if (resultValue)
                {
                    var resModel = userEducation.Adapt<UserEducationResponseModel>();
                    return resModel;
                }
            }


            return null;
        }

    }
}
