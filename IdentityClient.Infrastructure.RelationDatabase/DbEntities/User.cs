using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityClient.Infrastructure.RelationDatabase
{
    public sealed class User : IdentityUser<Guid>
    {
        public string PersonalId { get; set; }
        public bool IsMarried { get; set; }
        public bool IsEmployed { get; set; }
        public decimal Salary { get; set; }
        public Address Address { get; set; }
    }
}
