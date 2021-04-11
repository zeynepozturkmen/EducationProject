using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Core.Entities
{
    public class UserEducationStatus : BaseEnum
    {
        public UserEducationStatus(string name, string description) : base(name)
        {
            Description = description;
        }
        public string Description { get; set; }
        public virtual List<UserEducation> UserEducationList { get; set; } = new List<UserEducation>();

    }
}
