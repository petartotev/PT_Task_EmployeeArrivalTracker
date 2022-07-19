using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppServer.V1.Contracts.Common
{
    public class Error
    {
        public Error()
        {
        }

        public Error(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
