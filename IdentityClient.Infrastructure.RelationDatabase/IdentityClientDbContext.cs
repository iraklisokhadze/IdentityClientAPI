using AutoMapper.Internal;
using IdentityServer4.Models;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using IdentityServer4.EntityFramework.Mappers;
using IdentityModel;

namespace IdentityClient.Infrastructure.RelationDatabase
{
    public class IdentityClientDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public IdentityClientDbContext() : base() { }
        public IdentityClientDbContext(DbContextOptions<IdentityClientDbContext> options, IServiceProvider serviceProvider) : base(options) { Seed(serviceProvider); }
        private void Seed(IServiceProvider serviceProvider)
        {

            var role = Roles.FirstOrDefault(r => r.NormalizedName == "USER");
            if (role == null)
            {
                role = new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = "User",
                    NormalizedName = "USER"
                };
                Roles.Add(role);
            }
            SaveChanges();

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasKey(u => u.Id);
            builder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<User>().HasIndex(u => u.PersonalId).IsUnique();
            builder.Entity<User>().Property(u => u.PersonalId).HasMaxLength(255).IsRequired();
            builder.Entity<User>().Property(u => u.Salary).HasColumnType("Money");



            builder.Entity<Address>().HasKey(a => a.Id);
            builder.Entity<Address>().Property(a => a.Country).HasMaxLength(255).IsRequired();
            builder.Entity<Address>().Property(a => a.City).HasMaxLength(255).IsRequired();
            builder.Entity<Address>().Property(a => a.Street).HasMaxLength(255).IsRequired();
            builder.Entity<Address>().Property(a => a.Building).HasMaxLength(255).IsRequired();
            builder.Entity<Address>().Property(a => a.Apartment).HasMaxLength(255).IsRequired();


            builder.Entity<User>().HasOne(u => u.Address).WithOne(a => a.User);

            builder.Model.GetEntityTypes()
                         .Where(e => e.GetTableName().StartsWith("AspNet"))
                         .ForAll(e => e.SetTableName(e.GetTableName()[6..]));
        }

        public DbSet<Address> Addresses { get; set; }
        public override DbSet<User> Users { get; set; }
    }
}
