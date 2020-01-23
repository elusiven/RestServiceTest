using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimation.Service.Common.Exceptions.EmailProcessing
{
    public class MissingElementException : EmailProcessingException
    {
        public MissingElementException()
        {
        }

        public MissingElementException(string message) : base(message)
        {
        }
    }
}
