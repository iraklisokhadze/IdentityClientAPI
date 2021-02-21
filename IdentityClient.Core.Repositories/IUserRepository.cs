using IdentityClient.Core.Models;
using IdentityClient.Core.Models.RequestModels;
using System.Threading.Tasks;

namespace IdentityClient.Core.Repositories
{
    public interface IUserRepository : IReposotpryBase<User>
    {
        Task<bool> LogIn(string userName, string PassswordHash);
    }
}
