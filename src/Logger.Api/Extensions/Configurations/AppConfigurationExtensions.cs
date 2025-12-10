using Logger.Api.Configurations.Security;
using Logger.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Logger.Api.Extensions.Configurations;

public static class AppConfigurationExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder LoadApplicationConfiguration()
        {
            ConfigurationManager config = builder.Configuration;
            IServiceCollection services = builder.Services;

            // Configure Db Connection from appsettings
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            // Configure Hashing options from appsettings
            services.Configure<HashingOptions>(
                config.GetSection("Hashing")
            );

            // Configure DataEncryption options from appsettings
            builder.Services.Configure<DataEncryptionOptions>(
                builder.Configuration.GetSection("DataEncryption")
            );

            return builder;
        }
    }
}
