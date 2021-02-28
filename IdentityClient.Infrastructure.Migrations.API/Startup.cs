using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using IdentityClient.Infrastructure.RelationDatabase;

namespace IdentityClient.Infrastructure.Migrations.API
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            services.AddDbContext<IdentityClientDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuthDbContext")
                                                                                 , b => b.MigrationsAssembly("IdentityClient.Infrastructure.Migrations.API")
                                                                                  ), ServiceLifetime.Transient);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
        }
    }
}
