using AutoMapper;
using IdentityClient.Core.Repositories;
using IdentityClient.Infrastructure.RelationDatabase;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace IdentityClient.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(IMapper mapper,
                              UserManager<User> userManager,
                              SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<bool> Create(Core.Models.User user)
        {
            user.Id = Guid.NewGuid();
            var dbUser = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(dbUser);

            return result.Succeeded;
        }

        public async Task<bool> LogIn(string userName, string PassswordHash)
        {

            var user = await _signInManager.UserManager.FindByNameAsync(userName);
            var t = await _signInManager.CheckPasswordSignInAsync(user, PassswordHash, true);

            var result = await _signInManager.PasswordSignInAsync(userName, PassswordHash, false, false);
            return result.Succeeded;
        }

        public async Task<bool> Edit(Core.Models.User user)
        {
            var dbUser = await _userManager.FindByIdAsync(user.Id.ToString());
            var result = await _userManager.UpdateAsync(_mapper.Map(user, dbUser));
            return result.Succeeded;
        }

        public async Task<bool> Delete(Core.Models.User user)
        {
            var dbUser = _mapper.Map<User>(user);
            var result = await _userManager.DeleteAsync(dbUser);
            return result.Succeeded;
        }

    }
}
