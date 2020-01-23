using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimation.Service.Common.Exceptions.EmailProcessing
{
    public class UnclosedTagException : EmailProcessingException
    {
        public UnclosedTagException()
        {
        }

        public UnclosedTagException(string message) : base(message)
        {
        }
    }
}
