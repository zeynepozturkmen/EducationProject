using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Core.Entities
{
    public class UserEductaion : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid EducationId { get; set; }
        public Education Education { get; set; }
        public Guid UserEducationStatusId { get; set; }
        public UserEducationStatus UserEducationStatus { get; set; }
        public bool IsCompleted { get; set; }

    }
}
