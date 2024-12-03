using Common.Models;
using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System;

namespace Infrastructure.Extensions
{
    public static class AddRefitClientExtension
    {
        public static void AddRefitClient<T, TOptions>(this IServiceCollection services) where T : class where TOptions : BaseApiOptions
        {
            services.AddRefitClient<T>().ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                var apiOptions = serviceProvider.GetRequiredService<IOptions<TOptions>>().Value;
                httpClient.BaseAddress = new Uri(apiOptions.BaseUrl);
                httpClient.Timeout = apiOptions.Timeout;
            });
        }
    }
}
