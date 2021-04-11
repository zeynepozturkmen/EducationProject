using EducationProject.Contract.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Contract.RequestModel.Education
{
    public class EducationModel : BaseRequestModel
    {
        public string Name { get; set; }
        public double Time { get; set; }
        public int Quota { get; set; }
        public double Cost { get; set; }
        public double TotalCost { get; set; }
        public Guid CategoryId { get; set; }
        public Guid EducationInformationId { get; set; }
    }
}
