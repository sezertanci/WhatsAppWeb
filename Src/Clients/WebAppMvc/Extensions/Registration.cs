using WebAppMvc.Infrastructure.Services;
using WebAppMvc.Infrastructure.Services.Interfaces;

namespace WebAppMvc.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            var webApiAddress = configuration["WebApiHost"];

            services.AddHttpClient("WebApiClient", client =>
            {
                client.BaseAddress = new Uri(webApiAddress);
            });

            services.AddScoped(sp =>
            {
                var clientFactory = sp.GetRequiredService<IHttpClientFactory>();

                return clientFactory.CreateClient("WebApiClient");
            });

            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
