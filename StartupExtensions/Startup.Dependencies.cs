using DemoApplication.BusinessService.Album;
using DemoApplication.BusinessService.Handler;
using DemoApplication.BusinessServices.Interfaces.Album;
using DemoApplication.BusinessServices.Interfaces.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace DemoApplication.StartupExtensions
{
    public static class DependenciesExtension
    {

        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserAlbumsBusinessService, UserAlbumsBusinessService>();
        }

        public static void ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IWebApiCallerService, WebApiCallerService>(client =>
            {
                client.BaseAddress = new Uri(configuration["JsonPlaceHolderApiConfiguration:ApiPrefix"]);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
            {
                
            });
        }
    }
}
