using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public Guid RecordUserId { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
