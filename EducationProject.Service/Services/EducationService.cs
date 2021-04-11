using EducationProject.Contract.Common;
using EducationProject.Contract.RequestModel.Education;
using EducationProject.Contract.ResponseModel.Category;
using EducationProject.Contract.ResponseModel.Education;
using EducationProject.Contract.ResponseModel.EducationContentType;
using EducationProject.Contract.ResponseModel.TeacherInformation;
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
        public async Task<List<EducationResponseModel>> GetAllEducationAsync()
        {

            var list = await GetAllAsync();

            var model = new List<EducationResponseModel>();

            model = list.Select(item => item.Adapt<EducationResponseModel>()).ToList();

            return model;

        }
        public async Task<EducationResponseModel> GetEducationByIdAsync(ByIdRequestModel model)
        {

            var education = await _dbContext.Education.Where(x => !x.IsDeleted && x.Id == model.Id).Include(x => x.EducationContentList).FirstOrDefaultAsync();

            var resModel = education.Adapt<EducationResponseModel>();

            if (education.EducationContentList.Count>0)
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
            var educationContentList = await _dbContext.EducationContent.Where(x => !x.IsDeleted && x.EducationId == model.Id).Include(x => x.EducationContentType).ToListAsync();

            var resModel = educationContentList.Select(item => item.Adapt<EducationContentResponseModel>()).ToList();

            return resModel;
        }
        public async Task<List<EducationContentResponseModel>> CreateEducationContentAsync(AddEducationContentRequestModel model)
        {
            var educationContent = model.Adapt<EducationContent>();
            educationContent.RecordUserId = model.UserId.Value;

            await _dbContext.EducationContent.AddAsync(educationContent);

            var resultValue = await _uow.CommitAsync();

            if (resultValue)
            {
                var reqModel = new ByIdRequestModel();
                reqModel.Id = educationContent.EducationId;

                var resModel = await GetAllEducationContentByEducationId(reqModel);
                return resModel;
            }
            return null;
        }

    }
}
