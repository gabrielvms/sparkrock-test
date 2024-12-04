using Common.Extensions;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.ApiOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ConfigureServices
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureServices(configuration);
            services.AddRefitClient<ICurrencyExchangeApiClient, CurrencyExchangeApiOptions>();
        }
    }
}
