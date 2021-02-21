using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace IdentityClient.Core.Models.RequestModels
{
    public class RegisterUserRequest
    {
        [Required]
        public string PersonalId { get; set; }
        [Required]
        public bool IsMerried { get; set; }
        [Required]
        public bool IsEmployed { get; set; }
        public decimal Salary { get; set; }
        [Required]
        public AddressRequest Address { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
