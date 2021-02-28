using AutoMapper;
using IdentityClient.Core.Repositories;
using IdentityClient.Infrastructure.RelationDatabase;
using System;
using System.Threading.Tasks;
using AddressModel = IdentityClient.Core.Models.Address;

namespace IdentityClient.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IdentityClientDbContext _usersDbContext;
        private readonly IMapper _mapper;

        public AddressRepository(IdentityClientDbContext usersDbContext, IMapper mapper)
        {
            _usersDbContext = usersDbContext;
            _mapper = mapper;
        }

        public async Task<bool> Create(AddressModel address)
        {
            var addressDb = _mapper.Map<Address>(address);
            _usersDbContext.Addresses.Add(addressDb);
            await _usersDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(AddressModel address)
        {
            var addressDb = _mapper.Map<Address>(address);
            _usersDbContext.Addresses.Update(addressDb);
            await _usersDbContext.SaveChangesAsync();
            return true;
        }
        public Task<bool> Delete(AddressModel address)
        {
            throw new NotImplementedException();
        }
    }
}
