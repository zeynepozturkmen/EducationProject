using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Core.Entities
{
    public class TeacherInformation : BaseEnum
    {
        public TeacherInformation(string name,string description):base(name)
        {
            description = Description;
        }
        public string Description { get; set; }
        public virtual List<Education> EducationList { get; set; } = new List<Education>();
    }
}
