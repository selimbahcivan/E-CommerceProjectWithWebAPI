using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Responses
{
    public class ErrorApiDataResponse<T> : ApiDataResponse<T>
    {
        public ErrorApiDataResponse(T data) : base(success: false)
        {
            Data = data;
        }
        public ErrorApiDataResponse(T data, string message) : base(success: false, message: message)
        {
            Data = data;
        }
    }
}
