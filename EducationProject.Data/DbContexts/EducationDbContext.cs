using EducationProject.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Data.DbContexts
{
    public class EducationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public EducationDbContext(DbContextOptions<EducationDbContext> options) : base(options)
        {

        }


    }
}
