using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityClient.Core.Models.ResponseModels
{
    public class Response<T> : IResponse<T>
    {
        private bool _succeeded = true;
        public Response() { }
        public Response(T obj)
        {
            Data = obj;
        }
        public Response(IEnumerable<IErrorMesage> errorMesages)
        {
            ErrorMesages = errorMesages;
        }
        public bool Succeeded
        {
            get { return _succeeded && (ErrorMesages == null || !ErrorMesages.Any()); }
            set { _succeeded = value; }
        }

        public T Data { get; set; }

        public IEnumerable<IErrorMesage> ErrorMesages { get; set; }
    }
}
