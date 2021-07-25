using System;
using System.IO;
using System.Runtime.Serialization;

namespace Doing.Common.Exceptions
{
    public class DoingException : Exception
    {

        public string Code { get; }

        public DoingException()
        {
        }

        public DoingException(string code)
        {
            Code = code;
        }

         public DoingException(string message, params object[] args) 
                : this(string.Empty, message, args)
        {
        }

        public DoingException(string code, string message, params object[] args) 
                : this(null, code, message, args)
        {
        }

        public DoingException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public DoingException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        
    }
}