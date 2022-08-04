using DemoApplication.ApiModels.Configuration;
using DemoApplication.StartupExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DemoApplication
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDependencies(_configuration);
            services.ConfigureHttpClients(_configuration);
            services.AddSingleton<IConfiguration>(_configuration);
            var appsettingSection = _configuration.GetSection("JsonPlaceHolderApiConfiguration");
            services.Configure<JsonplaceholderConfiguration>(appsettingSection);
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(sg =>
            {
                sg.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo Api v1");
            });
        }
    }
}
