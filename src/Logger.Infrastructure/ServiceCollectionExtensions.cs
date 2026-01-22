using System.Reflection;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Services;
using Logger.Infrastructure.Configurations.Security;
using Logger.Infrastructure.CQRS.Messaging;
using Logger.Infrastructure.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Logger.Infrastructure.CQRS.Decorators;
using Logger.Application.CQRS.Decorators;

namespace Logger.Infrastructure;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructureServices(IConfiguration configuration, params Assembly[] assembliesToScan)
        {
            services.AddSingleton<ICacheService, RedisCacheService>();

            services.Configure<HashingOptions>(configuration.GetSection("Hashing"));
            services.AddSingleton<IHashingService, HashingService>();

            services.Configure<DataEncryptionOptions>(configuration.GetSection("DataEncryption"));
            services.AddSingleton<IDataEncryptionService, AesGcmDataEncryptionService>();

            services.AddCqrsWithValidation(assembliesToScan);

            return services;
        }

        private void AddCqrsWithValidation(Assembly[] assembliesToScan)
        {
            services.AddScoped<IMediator, Mediator>();

            foreach (Assembly assembly in assembliesToScan)
            {
                services.Scan(scan => scan
                    .FromAssemblies(assembly)
                    .AddClasses(classes => classes.AssignableTo(typeof(IHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                );

                services.Scan(scan => scan
                    .FromAssemblies(assembly)
                    .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                );
            }

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RetryBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
