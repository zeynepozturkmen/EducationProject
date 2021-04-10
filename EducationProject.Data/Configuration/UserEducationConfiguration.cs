using EducationProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Data.Configuration
{
    public class UserEducationConfiguration : BaseConfiguration<UserEducation>
    {
        public override void Configure(EntityTypeBuilder<UserEducation> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Education)
          .WithMany(x => x.UserEducationList)
          .HasForeignKey(x => x.EducationId);

            builder.HasOne(x => x.User)
            .WithMany(x => x.UserEducationList)
            .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.UserEducationStatus)
          .WithMany(x => x.UserEducationList)
          .HasForeignKey(x => x.UserEducationStatusId);

        }
    }
}

