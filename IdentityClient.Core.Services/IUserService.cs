using IdentityClient.Core.Models;
using IdentityClient.Core.Models.RequestModels;
using System.Threading.Tasks;

namespace IdentityClient.Core.Services
{
    public interface IUserService : IServiceBase<User>
    {
        Task<bool> LogIn(string userName, string PassswordHash);
    }
}
