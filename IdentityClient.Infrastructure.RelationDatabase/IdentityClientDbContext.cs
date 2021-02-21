using AutoMapper.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace IdentityClient.Infrastructure.RelationDatabase
{
    public class UsersDbContext : IdentityDbContext<User, Role, Guid>
    {
        public UsersDbContext() : base() { }
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { Seed(); }
        private void Seed()
        {
            SaveChanges();

            // Make sure we have a SysAdmin role
            var role = Roles.FirstOrDefault(r => r.NormalizedName == "USER");
            if (role == null)
            {
                role = new Role
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
        public override DbSet<Role> Roles { get; set; }
    }
}
