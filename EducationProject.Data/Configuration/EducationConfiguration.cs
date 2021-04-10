using EducationProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Data.Configuration
{
    public class EducationConfiguration : BaseConfiguration<Education>
    {
        public override void Configure(EntityTypeBuilder<Education> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Category)
          .WithMany(x => x.EducationList)
          .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.EducationInformation)
            .WithMany(x => x.EducationList)
            .HasForeignKey(x => x.EducationInformationId);

            builder.Metadata.FindNavigation(nameof(Education.EducationContentList)).SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.Metadata.FindNavigation(nameof(Education.UserEducationList)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
