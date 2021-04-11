using EducationProject.Contract.RequestModel.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.UI.Validators
{
    public class UserLoginValidator : AbstractValidator<LoginRequestModel>
    {
        public UserLoginValidator()
        {
            RuleFor(t => t.Email).NotEmpty().EmailAddress().WithMessage("Email cannot be blank!");
            RuleFor(t => t.Password).NotEmpty().WithMessage("Password  cannot be blank!");

        }
    }
}
