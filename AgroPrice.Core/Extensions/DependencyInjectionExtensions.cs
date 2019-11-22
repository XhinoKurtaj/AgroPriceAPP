using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AgroPrice.Core.Data;
using AgroPrice.Core.Services.Settings;
using AgroPrice.Core.Infrastructure.Sources;

namespace AgroPrice.Core.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void ConfigureDefaultOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(ISettings).IsAssignableFrom(p) && !p.IsInterface);

            foreach (var setting in settings)
            {
                var method = typeof(OptionsConfigurationServiceCollectionExtensions)
                    .GetMethods()
                    .Single(m => m.Name == "Configure"
                                 && m.IsGenericMethod
                                 && m.GetParameters().Count() == 2 //the overload that takes 0 parameters i.e. SomeMethod()
                                 && m.GetGenericArguments().Count() == 1 //the overload like SomeMethod<OnlyOneGenericParam>()
                    )
                    .MakeGenericMethod(new[] { setting })
                    .Invoke(null, new object[] { services, configuration.GetSection(setting.Name) });
            }
        }

        public static void AddSqlServerContext(this IServiceCollection services, string connectionString, string migrationAssembly)
        {
            services.AddDbContext<EntityDbContext>(optionsBuilder =>
            {
                var dbContextOptionsBuilder = optionsBuilder.UseLazyLoadingProxies();
                dbContextOptionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationAssembly));
            });
        }

        public static void AddSilver(this ContainerBuilder builder)
        {
            // data -----
            // database context
            builder.RegisterType<EntityDbContext>()
                .As<IDbContext>()
                .InstancePerLifetimeScope();

            // repository
            builder.RegisterGeneric(typeof(EntityRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();


            // add settings
            builder.RegisterGeneric(typeof(SettingService<>))
                .As(typeof(ISettingService<>))
                .InstancePerLifetimeScope();



            // sources -----
            // register all settings
            builder.RegisterSource(new SettingsSource());
        }
    }
}
