using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SampleApp
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
            services.AddControllers();
            
            services
                .AddResolver<int, IWeatherForecast>();
            
            services
                .AddNamedSingleton<int,IWeatherForecast>
                (
                    provider => new WeatherForecast(DateTime.Now, 33, "Present temperature in Northfield, OH"),
                    44067
                );

            services
                .AddNamedSingleton<int, IWeatherForecast>
                (
                    provider => new WeatherForecast(DateTime.Now, 34, "Present temperature in Brooklyn, OH"),
                    44144
                );

            services
                .AddNamedSingleton<int, IWeatherForecast>
                (
                    provider => new WeatherForecast(DateTime.Now, 32, "Present temperature in Mayfield Village, OH"),
                    44143
                );
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
        }
    }
}
