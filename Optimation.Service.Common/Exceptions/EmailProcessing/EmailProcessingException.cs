using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimation.Service.Common.Exceptions.EmailProcessing
{
    public class EmailProcessingException : Exception
    {
        public EmailProcessingException()
        {
        }

        public EmailProcessingException(string message) : base(message)
        {
        }
    }
}
