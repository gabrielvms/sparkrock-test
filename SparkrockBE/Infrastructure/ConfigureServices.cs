﻿using Core.Interfaces;
using Infrastructure.ApiOptions;
using Infrastructure.Extensions;
using Infrastructure.Services;
using Interface.ApiClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.Configure<CurrencyExchangeApiOptions>(configuration.GetSection(CurrencyExchangeApiOptions.SettingsSection));
            services.AddRefitClient<ICurrencyExchangeApiClient, CurrencyExchangeApiOptions>();
            services.AddSingleton<ICacheService, CacheService>();
        }
    }
}
