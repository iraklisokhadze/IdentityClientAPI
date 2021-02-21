using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityClient.Core.Models.RequestModels
{
    public class EditAddressRequest
    {
        [Required]
        public int Id { get; set; }
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
        [Required]
        public Guid UserId { get; set; }
    }
}
