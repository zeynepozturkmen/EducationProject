using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Contract.ResponseModel.Education
{
    public class EducationContentResponseModel
    {
        public Guid Id { get; set; }
        public Guid EducationId { get; set; }
        public string EducationContenTypeName{ get; set; }
        public int RowNumber { get; set; }
        public string FilePath { get; set; }
        public string BookContent { get; set; }
        public bool IsBack { get; set; }
        public bool IsNext { get; set; }
        public Guid UserEducationId { get; set; }
    }
}
