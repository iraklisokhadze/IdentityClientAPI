using IdentityClient.Core.Repositories;
using IdentityClient.Core.Services;
using IdentityClient.Infrastructure.Repositories;
using IdentityClient.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace IdentityClient.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            string connectionString = Configuration.GetConnectionString("AuthDbContext");

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityClient.API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = Configuration.GetValue<Uri>("Identity:Swagger:Authority"),
                            TokenUrl = Configuration.GetValue<Uri>("Identity:Swagger:Token"),
                            Scopes = new Dictionary<string, string>
                             {
                                 {"identity_client_api", "identity_client_api - full access"}
                             }
                        }
                    }
                });
            });

            services.AddDbContext<Infrastructure.RelationDatabase.IdentityClientDbContext>
               (options => options.UseSqlServer(Configuration.GetConnectionString("AuthDbContext")));

            services.AddIdentity<Infrastructure.RelationDatabase.User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<Infrastructure.RelationDatabase.IdentityClientDbContext>();

            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication("Bearer", options =>
                    {
                        options.ApiName = Configuration.GetValue<string>("Identity:Swagger:ClientId");
                        options.ApiSecret = Configuration.GetValue<string>("Identity:Swagger:Secret");
                        options.Authority = Configuration.GetValue<string>("Identity:Swagger:Authority");
                    });


            services.AddAutoMapper(typeof(Core.Models.RequestModels.Mapper));
            services.AddAutoMapper(typeof(Infrastructure.RelationDatabase.Mapper));
            services.AddControllersWithViews();
            //Injectint Repositories
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            //Injecting Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAddressService, AddresService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
             
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "identity_client_api V1");

                options.OAuthClientId("identity_client_api");
                options.OAuthAppName("identity_client_api - Swagger");
                options.OAuthUsePkce();
            });

            app.UseHttpsRedirection();
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
