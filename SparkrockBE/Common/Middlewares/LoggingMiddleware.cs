using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Common.Middlewares
{
    public static class LoggingMiddleware
    {
        public static void AddLogging(this WebApplicationBuilder builder)
        {
            builder.Logging
                .ClearProviders()
                .AddConsole()
                .AddDebug();
        }
    }
}
