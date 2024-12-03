using Infrastructure.ApiOptions;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Extensions
{
    public static class AddInfrastructureExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<ICurrencyExchangeApiClient, CurrencyExchangeApiOptions>();
        }
    }
}
