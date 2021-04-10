using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Core.Entities
{
    public class Education : BaseEntity
    {
        public string Name { get; set; }
        public double Time { get; set; }
        public int Quota { get; set; }
        public double Cost { get; set; }
        public double TotalCost { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid EducationInformationId { get; set; }
        public TeacherInformation EducationInformation { get; set; }
        public virtual List<EducationContent> EducationContentList { get; set; } = new List<EducationContent>();
        public virtual List<UserEducation> UserEducationList { get; set; } = new List<UserEducation>();
    }
}
