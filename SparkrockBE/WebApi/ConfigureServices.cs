namespace WebApi
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddMemoryCache()
                .AddControllers();
            return services;
        }
    }
}
