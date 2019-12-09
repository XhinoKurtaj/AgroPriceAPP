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
            RuleFor(model => model.Address).NotEmpty().WithMessage("Ju lutem vendosni adresen");
        }

    }
}
