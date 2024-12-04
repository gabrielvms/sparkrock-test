using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Common.Extensions
{
    public static class AppSettingsExtensions
    {
        public static void AddEnvironmentConfig(this WebApplicationBuilder builder)
        {
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                 .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                                 .AddEnvironmentVariables();

        }
    }
}
