using System.Threading.Tasks;

namespace IdentityClient.Core.Services
{
    public interface IServiceBase<T>
    {
        Task<bool> Create(T model);
        Task<bool> Edit(T model);
        Task<bool> Delete(T model);
    }
}
