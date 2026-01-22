using Logger.Application.Configurations.Security;
using Logger.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logger.Application;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationServices(IConfiguration configuration)
        {
            services.Configure<SessionManagementOptions>(configuration.GetSection("SessionManagement"));
            services.AddScoped<SessionManagementService>();

            return services;
        }
    }
}
