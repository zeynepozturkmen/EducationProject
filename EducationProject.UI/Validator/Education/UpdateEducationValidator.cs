using EducationProject.Contract.RequestModel.Education;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationProject.UI.Validator.Education
{
    public class UpdateEducationValidator : AbstractValidator<UpdateEducationRequestModel>
    {
        public UpdateEducationValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("Education name cannot be blank!");
            RuleFor(t => t.Time).NotEmpty().WithMessage("Time  cannot be blank!");
            RuleFor(t => t.Quota).NotEmpty().WithMessage("Quota  cannot be blank!");
            RuleFor(t => t.CategoryId).NotEmpty().WithMessage("Category cannot be blank!");
            RuleFor(t => t.EducationInformationId).NotEmpty().WithMessage("Teacher information cannot be blank!");
        }
    }
}
