using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimation.Shared.Exceptions.Extensions
{
    public class ExtensionsException : Exception
    {
        public ExtensionsException()
        {
        }

        public ExtensionsException(string message) : base(message)
        {
        }

        public ExtensionsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
