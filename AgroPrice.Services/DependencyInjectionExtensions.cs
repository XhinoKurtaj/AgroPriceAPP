using AgroPrice.Services.Account;
using AgroPrice.Services.PointOfSale;
using AgroPrice.Services.WholeSaleMarket;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Services.Mail;
using AgroPrice.Services.Product;

namespace AgroPrice.Services
{
    public static class DependencyInjectionExtensions
    {
        public static void AddServices(this ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<WholeSaleMarketService>().As<IWholeSaleMarketService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PointOfSaleService>().As<IPointOfSaleService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<MailService>().As<IMailService>()
                .InstancePerLifetimeScope();
        }
    }
}
