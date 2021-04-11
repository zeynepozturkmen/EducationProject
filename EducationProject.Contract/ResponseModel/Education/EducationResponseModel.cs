using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Contract.ResponseModel.Education
{
    public class EducationResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Time { get; set; }
        public int Quota { get; set; }
        public double Cost { get; set; }
        public double TotalCost { get; set; }
        public string CategoryName { get; set; }
        public List<EducationContentResponseModel> EducationContentList { get; set; } = new List<EducationContentResponseModel>();
    }
}
