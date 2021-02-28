using IdentityClient.Core.Models;
using System.Threading.Tasks;

namespace IdentityClient.Core.Repositories
{
    public interface IUserRepository : IReposotpryBase<User>
    {
        Task<bool> LogIn(string userName, string PassswordHash);
    }
}
