using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityClient.Core.Models
{
    public sealed class User : IdentityUser<Guid>
    {
        public string PersonalId { get; set; }
        public bool IsMarried { get; set; }
        public bool IsEmployed { get; set; }
        public decimal Salary { get; set; }
        public bool Deleted { get; set; }
        public Address ResidentialAddress { get; set; }

   
    }
}
