using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure;

namespace Application
{
    public static class ConfigureServices
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureServices(configuration);
            services.AddSingleton<ICurrencyExchangeService, CurrencyExchangeService>();
        }
    }
}
