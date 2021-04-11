using EducationProject.Contract.Common;
using EducationProject.Contract.RequestModel.Education;
using EducationProject.Contract.ResponseModel.Category;
using EducationProject.Contract.ResponseModel.Education;
using EducationProject.Contract.ResponseModel.EducationContentType;
using EducationProject.Contract.ResponseModel.TeacherInformation;
using EducationProject.Core.Entities;
using EducationProject.Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationProject.Service.IServices
{
    public interface IEducationService : IBaseService<Education>
    {
        Task<List<EducationResponseModel>> GetAllEducationAsync();
        Task<EducationResponseModel> CreateEducationAsync(EducationModel model);
        List<TeacherEducationResponseModel> GetAllTeacherInformation();
        List<CategoryResponseModel> GetAllCategory();
        Task<EducationResponseModel> GetEducationByIdAsync(ByIdRequestModel model);
        List<EducationContentTypeResponseModel> GetAllEducationContentType();
        Task<List<EducationContentResponseModel>> CreateEducationContentAsync(AddEducationContentRequestModel model);
        Task<List<EducationContentResponseModel>> GetAllEducationContentByEducationId(ByIdRequestModel model);

    }
}
