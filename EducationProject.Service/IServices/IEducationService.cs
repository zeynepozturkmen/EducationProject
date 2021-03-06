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
        List<EducationResponseModel> GetAllEducation();
        Task<EducationResponseModel> CreateEducationAsync(EducationModel model);
        List<TeacherEducationResponseModel> GetAllTeacherInformation();
        List<CategoryResponseModel> GetAllCategory();
        Task<EducationResponseModel> GetEducationByIdAsync(Guid Id);
        List<EducationContentTypeResponseModel> GetAllEducationContentType();
        Task<List<EducationContentResponseModel>> CreateEducationContentAsync(AddEducationContentRequestModel model);
        Task<List<EducationContentResponseModel>> GetAllEducationContentByEducationId(ByIdRequestModel model);
        Task<EducationContentResponseModel> DeleteEducationContentAsync(Guid EducationContentId);
        Task<EducationResponseModel> DeleteEducationAsync(Guid EducationId);
        Task<UpdateEducationRequestModel> GetEducationModelByIdAsync(Guid Id);
        Task<EducationResponseModel> UpdateEducationAsync(UpdateEducationRequestModel model);
        Task<EducationContentResponseModel> GetEducationContentByIdAsync(Guid Id);

    }
}
