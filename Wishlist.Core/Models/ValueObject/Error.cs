using System;
using System.Collections.Generic;
using System.Text;

namespace Wishlist.Core.Models.ValueObject
{
    public struct Error
    {
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
           
        }
        public string Code { get; }
        public string Message { get; }       
    }
}
