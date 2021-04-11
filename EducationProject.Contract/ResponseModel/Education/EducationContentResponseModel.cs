using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Contract.ResponseModel.Education
{
    public class EducationContentResponseModel
    {
        public Guid Id { get; set; }
        public Guid EducationId { get; set; }
        public string EducationContenType{ get; set; }
        public int RowNumber { get; set; }
        public string FilePath { get; set; }
    }
}
