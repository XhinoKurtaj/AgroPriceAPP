using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Services.Account.Models
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        private readonly UserManager<IdentityUser> _userManager;
        public RegisterViewModelValidator(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            RuleFor(model => model.Name).NotEmpty().WithMessage("Ju lutem vendosni emrin").MaximumLength(255).WithMessage("Emri shume i gjate");
            RuleFor(model => model.Password).NotEmpty().WithMessage("Ju lutem vendosni passwordin").MinimumLength(8)
                      .MaximumLength(16).WithMessage("Password-i mund te jete nga 8-16 karaktere.");
            RuleFor(model => model.Email).EmailAddress().WithMessage("Ju lutem vendosni si duhet email-in").NotEmpty().WithMessage("Ju lutem vendosni email-in").MaximumLength(255).WithMessage("Email shume i gjate");
           
            RuleFor(model => model.PhoneNumber).NotEmpty().WithMessage("Ju lutem vendosni numrin e telefonit");
           
            RuleFor(model => model.Password).Must((model, confirmationPassword)
                  => PasswordConfirmation(model.Password, model.ConfirmationPassword)).WithMessage("Fjalekalimi nuk perputhet");
        }

        public bool PasswordConfirmation(string password, string confirmationPassword)
        {
            return password == confirmationPassword;
        }

    }
}
