using EducationProject.Contract.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using static EducationProject.Core.Constants.Constants;

namespace EducationProject.Contract.RequestModel.Education
{
    public class AddEducationContentRequestModel : BaseRequestModel
    {
        public Guid EducationId { get; set; }
        public int RowNumber { get; set; }
        public IFormFile File { get; set; }
        public string FilePath { get; set; }
        public string BookContent { get; set; }
        public EducationContentType EducationContenType { get; set; }
    }
}
