using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Services.PointOfSale.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace AgroPrice.Services.Product.Models
{
    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public ProductModelValidator()
        {
           
            RuleFor(model => model.Origin).NotEmpty()
                .WithMessage("Ju lutem plotesoni origjinen e produktit");
            RuleFor(model => model.Quantity).NotEmpty()
                .WithMessage("Ju lutem plotesoni sasine e produktit");
            RuleFor(model => model.Price).NotEmpty()
                .WithMessage("Ju lutem plotesoni cmimin e produktit");

        }
    }
}
