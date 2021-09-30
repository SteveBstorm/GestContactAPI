using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using ConnectionTool;
using DataAccessLayer.Interface;
using DataAccessLayer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestContactAPI
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

            /*
             Singleton => Une seule instance du service pour la durée de vie de l'application
             Scoped => Une nouvelle instance à chaque appel client 
                    => Chaque fois qu'un client va interroger le controller, l'instance de services va être crée pour
                       toute la durée de l'appel
             Transient => Une nouvelle instance à chaque appel du service
             */
            services.AddSingleton(sp => new Connection(Configuration.GetConnectionString("default")));

            services.AddScoped<IContactGlobalRepo, ContactGlobalService>();
            services.AddScoped<IUserGlobalRepo, UserGlobalService>();

            services.AddScoped<IContactClientRepo, ContactClientService>();
            services.AddScoped<IUserClientRepo, UserClientService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GestContactAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestContactAPI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
