using IdentityClient.Core.Models;
using System.Threading.Tasks;

namespace IdentityClient.Core.Services
{
    public interface IUserService : IServiceBase<User>
    {
        Task<bool> LogIn(string userName, string PassswordHash);
    }
}
