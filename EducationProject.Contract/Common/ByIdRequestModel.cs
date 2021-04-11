using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Contract.Common
{
    public class ByIdRequestModel : BaseRequestModel
    {
        public Guid Id { get; set; }
    }
}
