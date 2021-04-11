using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Core.Entities
{
    public class EducationContentType : BaseEnum
    {
        public EducationContentType(string name, string description) : base(name)
        {
            Description = description;
        }
        public string Description { get; set; }
        public virtual List<EducationContent> EducationContentList { get; set; } = new List<EducationContent>();
    }
}
