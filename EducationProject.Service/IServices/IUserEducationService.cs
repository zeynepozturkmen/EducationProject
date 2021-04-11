using EducationProject.Contract.Common;
using EducationProject.Contract.ResponseModel.Education;
using EducationProject.Contract.ResponseModel.UserEducation;
using EducationProject.Core.Entities;
using EducationProject.Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationProject.Service.IServices
{
    public interface IUserEducationService : IBaseService<UserEducation>
    {
        List<EducationResponseModel> GetAllEducation();
        Task<EducationResponseModel> BuyEducation(ByIdRequestModel model);
        List<UserEducationResponseModel> GetAllEducationByUserId(ByIdRequestModel model);
        List<UserEducationResponseModel> GetAllCompletedEducationByUserId(ByIdRequestModel model);
        Task<EducationContentResponseModel> StartEducationByEducationId(ByIdRequestModel model);
        Task<EducationContentResponseModel> NextByEducationContentId(Guid Id, Guid UserEducationId);
        Task<EducationContentResponseModel> BackByEducationContentId(Guid Id, Guid UserEducationId);
        Task<EducationContentResponseModel> ContinueEducationByUserEducationId(ByIdRequestModel model);
        Task CompletedEducation(ByIdRequestModel model);
        Task TrainingEducation(ByIdRequestModel model);
    }
}
