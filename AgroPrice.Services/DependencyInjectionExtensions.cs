using AgroPrice.Services.Account;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Services
{
    public static class DependencyInjectionExtensions
    {
        public static void AddServices(this ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>().As<IAccountService>()
                .InstancePerLifetimeScope();
        }
    }
}
