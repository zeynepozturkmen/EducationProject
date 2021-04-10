using EducationProject.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Data.Configuration
{
    public class EducationContentConfiguration : BaseConfiguration<EducationContent>
    {
        public override void Configure(EntityTypeBuilder<EducationContent> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Education)
          .WithMany(x => x.EducationContentList)
          .HasForeignKey(x => x.EducationId);

            builder.HasOne(x => x.EducationContentType)
            .WithMany(x => x.EducationContentList)
            .HasForeignKey(x => x.EducationContenTypetId);

        }
    }
}
