using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityClient.Core.Models.RequestModels
{
    public class EditUserRequest 
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string PersonalId { get; set; }
        [Required]
        public bool IsMerried { get; set; }
        [Required]
        public bool IsEmployed { get; set; }
        public decimal Salary { get; set; }
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
        [Required]
        public EditAddressRequest Address { get; set; }
    }
}