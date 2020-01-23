using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimation.Shared.Exceptions.Extensions
{
    public class StringExtensionException : ExtensionsException
    {
        public StringExtensionException()
        {
        }

        public StringExtensionException(string message) : base(message)
        {
        }

        public StringExtensionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
