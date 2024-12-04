using Microsoft.Extensions.Configuration;

namespace Common.Helpers
{
    public class AuthHeaderHandler(IConfiguration configuration) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = configuration.GetValue<string>("Tokens:FXRatesToken");

            if (!string.IsNullOrEmpty(token))
            {
                // Add the Bearer token to the Authorization header
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            // Continue with the request
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
