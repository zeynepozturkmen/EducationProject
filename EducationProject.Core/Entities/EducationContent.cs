using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Core.Entities
{
    public class EducationContent : BaseEntity
    {
        public Guid EducationId { get; set; }
        public Education Education { get; set; }
        public Guid EducationContenTypetId { get; set; }
        public EducationContentType EducationContentType { get; set; }
        public int RowNumber { get; set; }
        public string FilePath { get; set; }
    }
}
