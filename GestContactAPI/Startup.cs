using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using ConnectionTool;
using DataAccessLayer.Interface;
using DataAccessLayer.Services;
using GestContactAPI.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            services.AddScoped<ITokenManager, TokenManager>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GestContactAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n                       Enter 'Bearer' [space] and then your token in the text input below.                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                         {
                           {
                             new OpenApiSecurityScheme 
                             {
                               Reference = new OpenApiReference 
                               {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer" 
                               },
                               Scheme = "oauth2",
                               Name = "Bearer",
                               In = ParameterLocation.Header
                             },
                             new List<string>()
                           }
                         });
            });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("isConnected", policy => policy.RequireAuthenticatedUser());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenManager.secret)),
                        ValidateIssuer = true,
                        ValidIssuer = TokenManager.myIssuer,
                        ValidateAudience = true,
                        ValidAudience = TokenManager.myAudience
                    };
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
