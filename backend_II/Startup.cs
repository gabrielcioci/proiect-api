using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_II.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace backend_II
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
            services.Configure<SamsariiDatabaseSettings>(
                Configuration.GetSection(nameof(SamsariiDatabaseSettings)));

            services.AddSingleton<ISamsariiDatabaseSettings>(provider => provider.GetRequiredService<IOptions<SamsariiDatabaseSettings>>().Value);

            services.AddScoped<UsersService>();
            services.AddScoped<ClientiService>();
            services.AddScoped<ReparatiiService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Make sure you call this before calling app.UseMvc()
            app.UseCors(
                options => options.WithOrigins("*").AllowAnyMethod()
            );

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
