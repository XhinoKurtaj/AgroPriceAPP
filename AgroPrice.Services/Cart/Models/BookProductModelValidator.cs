using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core.Data;
using AgroPrice.Services.Product.Models;
using FluentValidation;

namespace AgroPrice.Services.Cart.Models
{
    public class BookProductModelValidator : AbstractValidator<BookProductModel>
    {
        private readonly IRepository<Domain.Domain.Product.Product> _products;
        public BookProductModelValidator(IRepository<Domain.Domain.Product.Product> products)
        {
            _products = products;
            RuleFor(m => m.BookQuantity).Must((model, bookQuantity)
                => ValidQuantity(model.Id,bookQuantity)).WithMessage("Vendosni vleren e sasine te sakte");
        }
        public bool ValidQuantity(Guid id, decimal quantity)
        {
            var product = _products.GetByIdAsync(id);
            //var saleQuantity = product.Result.Quantity;
            return true; //TODO
            //return ((quantity>0)&&(quantity<=saleQuantity));
        }
    }
}
