﻿using Common.Helpers;
using Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace Infrastructure.Extensions
{
    public static class AddRefitClientExtension
    {
        public static void AddRefitClient<T, TOptions>(this IServiceCollection services) where T : class where TOptions : BaseApiOptions
        {
            services.AddSingleton<HttpLoggingHandler>();
            services.AddSingleton<AuthHeaderHandler>();

            services
                .AddRefitClient<T>().ConfigureHttpClient((serviceProvider, httpClient) =>
                {
                    var apiOptions = serviceProvider.GetRequiredService<IOptions<TOptions>>().Value;
                    httpClient.BaseAddress = new Uri(apiOptions.BaseUrl);
                    httpClient.Timeout = apiOptions.Timeout;
                })
                .AddHttpMessageHandler<HttpLoggingHandler>()
                .AddHttpMessageHandler<AuthHeaderHandler>();
        }
    }
}
