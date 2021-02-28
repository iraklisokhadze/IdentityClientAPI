using System.ComponentModel.DataAnnotations;

namespace IdentityClient.Core.Models.RequestModels
{
    public class AddAddressRequest
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Building { get; set; }
        [Required]
        public string Apartment { get; set; }
    }
}
