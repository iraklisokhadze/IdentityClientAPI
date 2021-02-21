using System.Threading.Tasks;

namespace IdentityClient.Core.Repositories
{
    public interface IReposotpryBase<T>
    {
        Task<bool> Create(T model);
        Task<bool> Edit(T model);
        Task<bool> Delete(T model);
    }
}
