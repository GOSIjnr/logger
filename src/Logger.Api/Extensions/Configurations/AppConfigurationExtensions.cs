using Logger.Api.Configurations.Security;
using Logger.Api.Data;
using Logger.Api.Interfaces.Services;
using Logger.Api.Services;
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
                options.UseSqlServer(config.GetConnectionString("Database"));
            });

            // Configure Hashing options from appsettings
            services.Configure<HashingOptions>(
                config.GetSection("Hashing")
            );

            services.AddSingleton<IHashingService, HashingService>();

            // Configure DataEncryption options from appsettings
            builder.Services.Configure<DataEncryptionOptions>(
                builder.Configuration.GetSection("DataEncryption")
            );

            services.AddSingleton<IDataEncryptionService, AesDataEncryptionService>();

            return builder;
        }
    }
}
