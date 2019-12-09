using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Services.PointOfSale.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace AgroPrice.Services.WholeSaleMarket.Models
{
    public class UpdateWholeSaleMarketModelValidator : AbstractValidator<UpdateWholeSaleMarketModel>
    {
        public UpdateWholeSaleMarketModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Ju lutem vendosni emrin e tregut").MaximumLength(255)
                .WithMessage("Emri shume i gjate");
            RuleFor(model => model.Address).NotEmpty().WithMessage("Ju lutem vendosni adresen"); RuleFor(model => model.Name).NotEmpty().WithMessage("Ju lutem vendosni emrin e tregut").MaximumLength(255)
                .WithMessage("Emri shume i gjate");
            RuleFor(model => model.Address).NotEmpty().WithMessage("Ju lutem vendosni adresen");
            RuleFor(model => model.PhoneNumber).NotEmpty().WithMessage("Ju lutem vendosni numrin e telefonit");
            RuleFor(model => model.Email).EmailAddress().WithMessage("Ju lutem vendosni si duhet email-in").NotEmpty()
                .WithMessage("Ju lutem vendosni email-in").MaximumLength(255).WithMessage("Email shume i gjate");
        }

    }
}
