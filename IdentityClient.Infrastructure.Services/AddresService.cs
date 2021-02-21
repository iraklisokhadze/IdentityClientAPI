using IdentityClient.Core.Models;
using IdentityClient.Core.Repositories;
using IdentityClient.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityClient.Infrastructure.Services
{
    public class AddresService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        public AddresService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<bool> Create(Address address) => await _addressRepository.Create(address);


        public async Task<bool> Delete(Address address) => await _addressRepository.Delete(address);
        public async Task<bool> Edit(Address address) => await _addressRepository.Edit(address);
    }
}
