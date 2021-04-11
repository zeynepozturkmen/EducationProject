using EducationProject.Contract.ResponseModel.Education;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Contract.ResponseModel.UserEducation
{
    public class UserEducationResponseModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EducationId { get; set; }
        public EducationResponseModel EducationResponseModel { get; set; } = new EducationResponseModel();
        public Guid UserEducationStatusId { get; set; }
        public int? LastWathedOrderNo { get; set; }
        public bool IsCompleted { get; set; }

    }
}
