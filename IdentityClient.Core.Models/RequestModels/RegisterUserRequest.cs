using System.ComponentModel.DataAnnotations;

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
        public AddAddressRequest Address { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
