using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace AgroPrice.Services.PointOfSale.Models
{
    public class UpdateSellerWithPointOfSaleModelValidator : AbstractValidator<UpdateSellerWithPointOfSaleModel>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UpdateSellerWithPointOfSaleModelValidator(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            RuleFor(model => model.Name).NotEmpty().WithMessage("Ju lutem vendosni emrin").MaximumLength(255)
                .WithMessage("Emri shume i gjate");
            RuleFor(model => model.Password).NotEmpty().WithMessage("Ju lutem vendosni passwordin").MinimumLength(8)
                .MaximumLength(16).WithMessage("Password-i mund te jete nga 8-16 karaktere.");
            RuleFor(model => model.Email).EmailAddress().WithMessage("Ju lutem vendosni si duhet email-in").NotEmpty()
                .WithMessage("Ju lutem vendosni email-in").MaximumLength(255).WithMessage("Email shume i gjate");
            RuleFor(model => model.WholeSaleMarketId).NotEmpty()
                .WithMessage("Ju lutem zgjidhni tregun ne te cilin ndodhet pika e shitjes");
            RuleFor(model => model.PhoneNumber).NotEmpty().WithMessage("Ju lutem vendosni numrin e telefonit");
            RuleFor(model => model.Password).Must((model, confirmationPassword)
                    => PasswordConfirmation(model.Password, model.ConfirmationPassword))
                .WithMessage("Fjalekalimi nuk perputhet");
            RuleFor(model => model.PointOfSaleDescription).NotEmpty()
                .WithMessage("Ju lutem vendosni pershkrimin e pikes se shitjes");
        }

        public bool PasswordConfirmation(string password, string confirmationPassword)
        {
            return password == confirmationPassword;
        }

    }
}
