using EducationProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Data.Configuration
{
    public class EducationContentTypeConfiguration : BaseConfiguration<EducationContentType>
    {
        public override void Configure(EntityTypeBuilder<EducationContentType> builder)
        {
            base.Configure(builder);

            builder.Metadata.FindNavigation(nameof(EducationContentType.EducationContentList)).SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}

