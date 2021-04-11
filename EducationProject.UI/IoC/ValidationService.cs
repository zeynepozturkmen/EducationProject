using EducationProject.Contract.RequestModel.Education;
using EducationProject.Contract.RequestModel.User;
using EducationProject.UI.Validator.Education;
using EducationProject.UI.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EducationProject.UI.IoC
{
    public class ValidationService
    {
        public static void Add(IServiceCollection services)
        {
            services.AddTransient<IValidator<LoginRequestModel>, UserLoginValidator>();
            services.AddTransient<IValidator<EducationModel>, EducationValidator>();
            services.AddTransient<IValidator<UpdateEducationRequestModel>, UpdateEducationValidator>();
        }
    }
}
