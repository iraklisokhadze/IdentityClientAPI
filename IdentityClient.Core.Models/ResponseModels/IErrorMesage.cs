using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityClient.Core.Models.ResponseModels
{
    public interface IErrorMesage
    {
        string Key { get; set; }
        string Value { get; set; }
    }
}
