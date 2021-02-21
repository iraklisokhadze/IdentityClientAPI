using AutoMapper;
using IdentityClient.Core.Repositories;
using IdentityClient.Infrastructure.RelationDatabase;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Cryptography;
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

        private string HashPassword(string password)
        {

            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public async Task<bool> LogIn(string userName, string PassswordHash)
        {
            var user = await _userManager.FindByNameAsync(userName);

            //var IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            //var IsPhoneNumberConfirmed = await _userManager.IsPhoneNumberConfirmedAsync(user);
            //await _userManager.AddToRoleAsync(user, "User");
            //var users = await _userManager.GetUsersInRoleAsync("User");

            //var result1 = await _signInManager.PasswordSignInAsync(userName, PassswordHash, false, false);

            var result = await _signInManager.PasswordSignInAsync(userName, PassswordHash, false, false);

            if (result.IsNotAllowed)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    // Email isn't confirmed.
                }

                if (!await _userManager.IsPhoneNumberConfirmedAsync(user))
                {
                    // Phone Number isn't confirmed.
                }
            }
            else if (result.IsLockedOut)
            {
                // Account is locked out.
            }
            else if (result.RequiresTwoFactor)
            {
                // 2FA required.
            }
            return result.Succeeded;
        }

        public async Task<bool> Edit(Core.Models.User user)
        {
            var dbUser = await _userManager.FindByIdAsync(user.Id.ToString());
            var user2 = _mapper.Map(user, dbUser);

            var result = await _userManager.UpdateAsync(dbUser);
            return result.Succeeded;
        }

        public async Task<bool> Delete(Core.Models.User user)
        {
            var dbUser = _mapper.Map<User>(user);
            var result = await _userManager.DeleteAsync(dbUser);
            return result.Succeeded;

            //_usersDbContext.Users.Remove(dbUser);
            //await _usersDbContext.SaveChangesAsync();
        }

    }
}
