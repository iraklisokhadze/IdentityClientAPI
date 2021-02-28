using System.Collections.Generic;

namespace IdentityClient.Core.Models.ResponseModels
{
    public interface IResponse<T>
    {
        bool Succeeded { get; }
        T Data { get; }
        IEnumerable<IErrorMesage> ErrorMesages { get; }
    }
}
