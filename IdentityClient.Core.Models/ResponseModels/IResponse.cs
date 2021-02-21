using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityClient.Core.Models.ResponseModels
{
    public interface IResponse<T>
    {
        bool Succeeded { get; }
        T Data { get; }
        IEnumerable<IErrorMesage> ErrorMesages { get; }
    }
}
