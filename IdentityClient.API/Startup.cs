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

namespace IdentityClient.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityClient.API", Version = "v1" });
            });
            services.AddDbContext<Infrastructure.RelationDatabase.UsersDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("AuthDbContext")));
            services.AddIdentity<Infrastructure.RelationDatabase.User, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<Infrastructure.RelationDatabase.UsersDbContext>()
                    .AddDefaultTokenProviders();



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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityClient.API v1"));
            }

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
