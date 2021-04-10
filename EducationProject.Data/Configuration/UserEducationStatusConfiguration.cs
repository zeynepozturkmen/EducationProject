using EducationProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace EducationProject.Data.Configuration
{
    public class UserEducationStatusConfiguration : BaseConfiguration<UserEducationStatus>
    {
        public override void Configure(EntityTypeBuilder<UserEducationStatus> builder)
        {
            base.Configure(builder);


            builder.Metadata.FindNavigation(nameof(UserEducationStatus.UserEducationList)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
