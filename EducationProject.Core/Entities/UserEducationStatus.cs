﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Core.Entities
{
    public class UserEducationStatus : BaseEnum
    {
        public UserEducationStatus(string name, string description) : base(name)
        {
            description = Description;
        }
        public string Description { get; set; }
        public virtual List<UserEductaion> UserEductaionList { get; set; } = new List<UserEductaion>();

    }
}
