using EducationProject.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EducationProject.Data.DbContexts
{
    public class EducationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public EducationDbContext(DbContextOptions<EducationDbContext> options) : base(options)
        {

        }

        public DbSet<Education> Education { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<TeacherInformation> TeacherInformation { get; set; }
        public DbSet<EducationContent> EducationContent { get; set; }
        public DbSet<EducationContentType> EducationContentType { get; set; }
        public DbSet<UserEducation> UserEducation { get; set; }
        public DbSet<UserEducationStatus> UserEducationStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
