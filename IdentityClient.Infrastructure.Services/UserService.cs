using IdentityClient.Core.Models;
using IdentityClient.Core.Repositories;
using IdentityClient.Core.Services;
using IdentityModel;
using System.Threading.Tasks;

namespace IdentityClient.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Create(User user) => await _userRepository.Create(user);
        public async Task<bool> LogIn(string userName, string PassswordHash) => await _userRepository.LogIn(userName, PassswordHash.ToSha256());
        public async Task<bool> Edit(User user) => await _userRepository.Edit(user);
        public async Task<bool> Delete(User user) => await _userRepository.Delete(user);

    }
}
