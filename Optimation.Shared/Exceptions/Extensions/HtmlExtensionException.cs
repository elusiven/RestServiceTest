using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimation.Shared.Exceptions.Extensions
{
    public class HtmlExtensionException : ExtensionsException
    {
        public HtmlExtensionException()
        {
        }

        public HtmlExtensionException(string message) : base(message)
        {
        }

        public HtmlExtensionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
