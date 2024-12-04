using Infrastructure.ApiOptions;
using Infrastructure.Extensions;
using Infrastructure.ApiClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CurrencyExchangeApiOptions>(configuration.GetSection(CurrencyExchangeApiOptions.SettingsSection));
            services.AddRefitClient<ICurrencyExchangeApiClient, CurrencyExchangeApiOptions>();
        }
    }
}
