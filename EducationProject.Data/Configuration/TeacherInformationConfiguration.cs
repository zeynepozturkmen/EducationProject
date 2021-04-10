using EducationProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Data.Configuration
{
    public class TeacherInformationConfiguration : BaseConfiguration<TeacherInformation>
    {
        public override void Configure(EntityTypeBuilder<TeacherInformation> builder)
        {
            base.Configure(builder);

            builder.Metadata.FindNavigation(nameof(TeacherInformation.EducationList)).SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
