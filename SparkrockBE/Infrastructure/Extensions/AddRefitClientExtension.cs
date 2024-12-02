using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class AddRefitClientExtension
    {
        public static void AddRefitClients(this IServiceCollection services)
        {
            services.AddRefitClient<IAssetApiClient, AssetManagementApiOptions>(endpointSuffix: "asset")
                .WithRetryPolicy<AssetManagementApiOptions>().AddHttpMessageHandler<AuthorizationHeaderAppTokenHandler>();
        }
    }
}
