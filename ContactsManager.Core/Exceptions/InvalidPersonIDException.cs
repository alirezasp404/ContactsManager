using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Core.Exceptions
{
    public class InvalidPersonIDException : ArgumentException
    {
        public InvalidPersonIDException() : base()
        {

        }
        public InvalidPersonIDException(string? message) : base(message)
        {

        }
        public InvalidPersonIDException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
