using LogProxy.API.Helper;
using LogProxy.Core.AppSettings;
using LogProxy.Core.Interfaces;
using LogProxy.Service;
using LogProxy.Service.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxy.API
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
            services.AddOptions();

            services.Configure<AppSettings>(Configuration);

            AddingClients(services);

            services.AddControllers();

            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });

            services.AddScoped<IMessageService, MessageService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void AddingClients(IServiceCollection services)
        {
            var appSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>().Value;

            var refitSettings = new RefitSettings()
            {

                AuthorizationHeaderValueGetter = () => Task.FromResult(appSettings.AirTableSettings.Key)
            };

            services.AddRefitClient<IAirTableClient>(refitSettings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(appSettings.AirTableSettings.URL));


        }
    }
}
