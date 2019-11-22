using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Services.Account.Models
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(model => model.UserName).NotEmpty().MaximumLength(256);
            RuleFor(model => model.Password).NotEmpty().MinimumLength(8)
                      .MaximumLength(16);
        }
    }
}
