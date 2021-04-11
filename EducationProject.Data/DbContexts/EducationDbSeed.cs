using EducationProject.Core.Constants;
using EducationProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EducationProject.Data.DbContexts
{
    public static class EducationDbSeed
    {
        private static void GetAndAddEnums<TEntity, TEnum>(this DbContext dbContext, bool create,
           IDictionary<TEnum, Guid> ids) where TEntity : BaseEnum
        {
            foreach (TEnum e in Enum.GetValues(typeof(TEnum)))
            {

                var name = e.ToString();
                var query = dbContext.Set<TEntity>().SingleOrDefault(q => q.Name == name);
                if (query != null)
                {
                    ids.Add(e, query.Id);
                }
                else if (create)
                {
                    var memberInfo = typeof(TEnum).GetMember(name)[0];
                    var hasDisplayAttribute = Attribute.IsDefined(memberInfo, typeof(DisplayAttribute));
                    var displayAttribute =
                        hasDisplayAttribute ? memberInfo.GetCustomAttribute<DisplayAttribute>() : null;

                    if (displayAttribute?.Description != null)
                    {
                        var newEntity =
                            (TEntity)Activator.CreateInstance(typeof(TEntity), name, displayAttribute.Description);
                        newEntity.RecordDate = DateTime.Now;
                        var entity = dbContext.Set<TEntity>().Add(newEntity).Entity;
                        dbContext.SaveChanges();
                        ids.Add(e, entity.Id);
                    }
                    else
                    {
                        var newEntity = (TEntity)Activator.CreateInstance(typeof(TEntity), name);
                        var entity = dbContext.Set<TEntity>().Add(newEntity).Entity;
                        dbContext.SaveChanges();
                        ids.Add(e, entity.Id);
                    }
                }
            }
        }


        public static async void Initialize(IServiceProvider serviceProvider, bool create = false)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<EducationDbContext>();

            dbContext.Database.Migrate();

            dbContext.GetAndAddEnums<Category, Constants.Category>(create, Constants.CategoryIds);
            dbContext.GetAndAddEnums<TeacherInformation, Constants.TeacherInformation>(create, Constants.TeacherInformationIds);
            dbContext.GetAndAddEnums<EducationContentType, Constants.EducationContentType>(create, Constants.EducationContentTypeIds);
            dbContext.GetAndAddEnums<UserEducationStatus, Constants.UserEducationStatus>(create, Constants.UserEducationStatusIds);
          
        }
    }

}
