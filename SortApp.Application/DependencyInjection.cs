
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SortApp.Application.Config;
using SortApp.Application.Logic.SortingAlgorithm;
using SortApp.Application.Service;
using System.Reflection;
using FluentValidation;

namespace SortApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); 
            services.AddSingleton(provider => configuration.Get<AppConfig>());
            services.AddSingleton(typeof(FileServiceConfig), p => p.GetRequiredService<AppConfig>().FileServiceConfig);
            services.AddSingleton<IFileService, FileService>();
            services.AddScoped<ISortingAlgorithmFactory, SortingAlgorithmFactory>();

            RegisterNestedOfInterface<ISortingAlgorithm>(services);

            return services;
        }

        private static void RegisterNestedOfInterface<T>(IServiceCollection services)
        {
            var iType = typeof(T);
            Assembly.GetExecutingAssembly().DefinedTypes
                .Where(p => p.GetInterfaces().Contains(iType))
                .ToList()
                .ForEach(t =>
                {
                    services.AddScoped(iType, t);
                });
        }
    }
}
