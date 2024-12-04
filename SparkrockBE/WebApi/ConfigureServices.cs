using Application;
using Application.Services;

namespace WebApi
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddMemoryCache();
            
            services.AddApplicationServices(configuration);
            services.AddSingleton<CurrencyExchangeService>();

            return services;
        }
    }
}
