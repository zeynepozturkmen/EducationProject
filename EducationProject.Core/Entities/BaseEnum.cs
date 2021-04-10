using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Core.Entities
{
    public class BaseEnum : BaseEntity
    {
        public BaseEnum(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
