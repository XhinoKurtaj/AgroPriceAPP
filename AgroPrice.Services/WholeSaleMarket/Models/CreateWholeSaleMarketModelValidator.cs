using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Services.PointOfSale.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace AgroPrice.Services.WholeSaleMarket.Models
{
    public class CreateWholeSaleMarketModelValidator : AbstractValidator<CreateWholeSaleMarketModel>
    {
        private readonly UserManager<IdentityUser> _userManager;
        public CreateWholeSaleMarketModelValidator(UserManager<IdentityUser> userManager)
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Ju lutem vendosni emrin e tregut").MaximumLength(255)
                .WithMessage("Emri shume i gjate");
            RuleFor(model => model.Address).NotEmpty().WithMessage("Ju lutem vendosni adresen");
            RuleFor(model => model.PhoneNumber).NotEmpty().WithMessage("Ju lutem vendosni numrin e telefonit");
            RuleFor(model => model.Password).Must((model, confirmationPassword)
                    => PasswordConfirmation(model.Password, model.ConfirmationPassword))
                .WithMessage("Fjalekalimi nuk perputhet");
            RuleFor(model => model.Password).NotEmpty().WithMessage("Ju lutem vendosni passwordin").MinimumLength(8)
                .MaximumLength(16).WithMessage("Password-i mund te jete nga 8-16 karaktere.");
            RuleFor(model => model.Email).EmailAddress().WithMessage("Ju lutem vendosni si duhet email-in").NotEmpty()
                .WithMessage("Ju lutem vendosni email-in").MaximumLength(255).WithMessage("Email shume i gjate");
        }
        public bool PasswordConfirmation(string password, string confirmationPassword)
        {
            return password == confirmationPassword;
        }

    }
}
