using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime RecordDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid RecordUserId { get; set; }
        public Guid UpdateUserId { get; set; }
        public bool IsDeleted { get; set; }
        public string Address { get; set; }
        public string Tc { get; set; }
        public string FullName { get; set; }
    }
}
