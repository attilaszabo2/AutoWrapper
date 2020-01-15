using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutoWrapper.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var isDevelopment = env.IsDevelopment();
            if (isDevelopment)
            {
                app.UseCors(options => options.WithOrigins("http://localhost:8080")
                                              .AllowAnyMethod()
                                              .AllowAnyHeader());
                app.UseDeveloperExceptionPage();
            }

            app.UseApiResponseAndExceptionWrapper(
                new AutoWrapperOptions
                {
                    IsApiOnly = true,
                    ShowStatusCode = true,
                    IsDebug = isDevelopment,
                    IgnoreNullValue = true,
                    EnableExceptionLogging = isDevelopment,
                    EnableResponseLogging = true,
                    UseCamelCaseNamingStrategy = true
                });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
