using Microsoft.AspNetCore.Identity;
using System;

namespace EducationProject.Core.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
