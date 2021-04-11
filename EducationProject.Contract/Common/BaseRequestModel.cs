using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Contract.Common
{
    public abstract class BaseRequestModel
    {
        public string IpAddress { get; set; }
        public Guid? UserId { get; set; }
    }
}
