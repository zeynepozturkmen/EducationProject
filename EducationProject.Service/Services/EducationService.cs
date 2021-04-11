using EducationProject.Contract.Common;
using EducationProject.Contract.RequestModel.Education;
using EducationProject.Contract.ResponseModel.Category;
using EducationProject.Contract.ResponseModel.Education;
using EducationProject.Contract.ResponseModel.EducationContentType;
using EducationProject.Contract.ResponseModel.TeacherInformation;
using EducationProject.Core.Constants;
using EducationProject.Core.Entities;
using EducationProject.Data.DbContexts;
using EducationProject.Service.IService;
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
    public class EducationService : BaseService<Education>, IEducationService
    {
        public EducationService(EducationDbContext db, IUnitOfWork uow) : base(db, uow)
        {

        }
        public async Task<EducationResponseModel> CreateEducationAsync(EducationModel model)
        {
            var education = model.Adapt<Education>();
            education.RecordUserId = model.UserId.Value;
            education.TotalCost = model.Cost * model.Time;

            await _dbContext.Education.AddAsync(education);

            var resultValue = await _uow.CommitAsync();

            if (resultValue)
            {
                var resModel = education.Adapt<EducationResponseModel>();
                return resModel;
            }
            return null;
        }
        public List<EducationResponseModel> GetAllEducation()
        {
            var list = _dbContext.Education.Where(x => !x.IsDeleted).Include(x => x.Category).ToList();

            var model = list.Select(item => item.Adapt<EducationResponseModel>()).ToList();

            return model;

        }
        public async Task<EducationResponseModel> GetEducationByIdAsync(Guid Id)
        {

            var education = await _dbContext.Education.Where(x => !x.IsDeleted && x.Id == Id).Include(x => x.EducationContentList).FirstOrDefaultAsync();

            var resModel = education.Adapt<EducationResponseModel>();

            if (education.EducationContentList.Count>0 && education!=null)
            {
                resModel.EducationContentList = education.EducationContentList.Select(item => item.Adapt<EducationContentResponseModel>()).ToList();
            }

           

            return resModel;

        }
        public List<CategoryResponseModel> GetAllCategory()
        {

            var list = _dbContext.Category.Where(x => !x.IsDeleted).ToList();

            var model = new List<CategoryResponseModel>();

            model = list.Select(item => item.Adapt<CategoryResponseModel>()).ToList();

            return model;

        }
        public List<TeacherEducationResponseModel> GetAllTeacherInformation()
        {

            var list = _dbContext.TeacherInformation.Where(x => !x.IsDeleted).ToList();

            var model = new List<TeacherEducationResponseModel>();

            model = list.Select(item => item.Adapt<TeacherEducationResponseModel>()).ToList();

            return model;

        }
        public List<EducationContentTypeResponseModel> GetAllEducationContentType()
        {

            var list = _dbContext.EducationContentType.Where(x => !x.IsDeleted).ToList();

            var model = new List<EducationContentTypeResponseModel>();

            model = list.Select(item => item.Adapt<EducationContentTypeResponseModel>()).ToList();

            return model;

        }
        public async Task<List<EducationContentResponseModel>> GetAllEducationContentByEducationId(ByIdRequestModel model)
        {
            var educationContentList = await _dbContext.EducationContent.Where(x => !x.IsDeleted && x.EducationId == model.Id).Include(x => x.EducationContentType).OrderBy(x=>x.RowNumber).ToListAsync();

            var resModel = educationContentList.Select(item => item.Adapt<EducationContentResponseModel>()).ToList();

            return resModel;
        }
        public async Task<List<EducationContentResponseModel>> CreateEducationContentAsync(AddEducationContentRequestModel model)
        {
            var educationContent = model.Adapt<EducationContent>();
            educationContent.RecordUserId = model.UserId.Value;

            var educationContentTypeName = model.EducationContenType.ToString();

            var educationContentType = await _dbContext.EducationContentType.Where(x => x.Name == educationContentTypeName && !x.IsDeleted).FirstOrDefaultAsync();

            if (educationContentType != null)
            {
                educationContent.EducationContentType = educationContentType;

                await _dbContext.EducationContent.AddAsync(educationContent);

                var resultValue = await _uow.CommitAsync();

                if (resultValue)
                {
                    var reqModel = new ByIdRequestModel();
                    reqModel.Id = educationContent.EducationId;

                    var resModel = await GetAllEducationContentByEducationId(reqModel);
                    return resModel;
                }

            }



            return null;
        }

        public async Task<EducationContentResponseModel> DeleteEducationContentAsync(Guid EducationContentId)
        {
            var educationContent = await _dbContext.EducationContent.Where(x => x.Id == EducationContentId && !x.IsDeleted).FirstOrDefaultAsync();

            if (educationContent!=null)
            {
                educationContent.IsDeleted = true;

                var resultValue = await _uow.CommitAsync();

                if (resultValue)
                {
                    var resModel = educationContent.Adapt<EducationContentResponseModel>();
                    return resModel;
                }
            }


            return null;
        }

        public async Task<EducationResponseModel> DeleteEducationAsync(Guid EducationId)
        {
            var education = await _dbContext.Education.Where(x => x.Id == EducationId && !x.IsDeleted).Include(x=>x.EducationContentList).FirstOrDefaultAsync();


            if (education != null)
            {
                education.IsDeleted = true;

                var userEducation = await _dbContext.UserEducation.Where(x => x.EducationId == education.Id && !x.IsDeleted).ToListAsync();

                foreach (var item in userEducation)
                {
                    item.IsDeleted = true;
                }

                foreach (var item in education.EducationContentList)
                {
                    item.IsDeleted = true;

                }

                var resultValue = await _uow.CommitAsync();

                if (resultValue)
                {
                    var resModel = education.Adapt<EducationResponseModel>();
                    return resModel;
                }
            }


            return null;
        }
        

    }
}
