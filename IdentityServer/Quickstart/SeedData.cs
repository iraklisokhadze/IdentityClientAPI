using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.Quickstart
{
    public class SeedData
    {

        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddOperationalDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
            });
            services.AddConfigurationDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
            });

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

                var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
                context.Database.Migrate();
                EnsureSeedData(context);
            }
        }
        public static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())

                context.Clients.Add(new Client
                {
                    ClientId = "identity_client_api",
                    ClientName = "Client Credentials Client",
                    ClientSecrets = { new Secret("123".Sha256()) }, // change me!

                    AllowedGrantTypes = { GrantType.AuthorizationCode, GrantType.ClientCredentials },
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = new[] { "https://localhost:44373/swagger/oauth2-redirect.html" },
                    AllowedCorsOrigins = new[] { "https://localhost:44373" },

                    AllowedScopes = { "identity_client_api" }
                }.ToEntity());

            if (!context.IdentityResources.Any())
                context.IdentityResources.AddRange(new[]
                {
                    new IdentityResource{
                          Name= "openid",
                          UserClaims = new List<string> { "sub" }
                    },
                    new IdentityResource{
                        Name = "role",
                        UserClaims = new List<string> {"role"}
                    }
                }.Select(i => i.ToEntity()));

            if (!context.ApiScopes.Any())
                context.ApiScopes.Add(new ApiScope("identity_client_api", "Full access to identityclientapi").ToEntity());


            if (!context.ApiResources.Any())
                context.ApiResources.Add(new ApiResource("identity_client_api")
                {
                    Scopes = { "identity_client_api" },
                    //ApiSecrets = new List<Secret> { new Secret("ScopeSecret".ToSha256()) },
                    //UserClaims = new List<string> { "role" }
                }.ToEntity());

            context.SaveChanges();
        }
    }
}
