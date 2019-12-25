using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Services.Account.Models;
using AgroPrice.Services.Cart.Models;
using AgroPrice.Services.PointOfSale.Models;
using AgroPrice.Services.Product.Models;
using AgroPrice.Services.WholeSaleMarket.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AgroPrice.Services
{
    public static class RegisterValidators
    {
        public static void RegisterAllValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<LoginViewModel>, LoginViewModelValidator>();
            services.AddTransient<IValidator<RegisterViewModel>, RegisterViewModelValidator>();
            services.AddTransient<IValidator<CreateSellerWithPointOfSaleModel>, CreateSellerWithPointOfSaleModelValidator>();
            services.AddTransient<IValidator<UpdateSellerWithPointOfSaleModel>, UpdateSellerWithPointOfSaleModelValidator>();
            services.AddTransient<IValidator<ProductModel>, ProductModelValidator>();
            services.AddTransient<IValidator<CreateWholeSaleMarketModel>, CreateWholeSaleMarketModelValidator>();
            services.AddTransient<IValidator<UpdateWholeSaleMarketModel>, UpdateWholeSaleMarketModelValidator>();
            services.AddTransient<IValidator<ChangePasswordModel>, ChangePasswordModelValidator>();
            services.AddTransient<IValidator<BookProductModel>, BookProductModelValidator>();
        }
    }
}
