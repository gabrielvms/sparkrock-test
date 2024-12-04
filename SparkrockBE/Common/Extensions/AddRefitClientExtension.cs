using Common.Helpers;
using Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace Common.Extensions
{
    public static class AddRefitClientExtension
    {
        public static void AddRefitClient<T, TOptions>(this IServiceCollection services) where T : class where TOptions : BaseApiOptions
        {
            services
                .AddRefitClient<T>().ConfigureHttpClient((serviceProvider, httpClient) =>
                {
                    var apiOptions = serviceProvider.GetRequiredService<IOptions<TOptions>>().Value;
                    httpClient.BaseAddress = new Uri(apiOptions.BaseUrl);
                    httpClient.Timeout = apiOptions.Timeout;
                })
                .AddHttpMessageHandler<HttpLoggingHandler>()
                .Services.AddSingleton<HttpLoggingHandler>();
        }
    }
}
