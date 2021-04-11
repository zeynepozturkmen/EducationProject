using EducationProject.Contract.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Contract.RequestModel.Education
{
    public class AddEducationContentRequestModel : BaseRequestModel
    {
        public Guid EducationId { get; set; }
        public Guid EducationContenTypeId { get; set; }
        public int RowNumber { get; set; }
        public IFormFile File { get; set; }
        public string FilePath { get; set; }
    }
}
