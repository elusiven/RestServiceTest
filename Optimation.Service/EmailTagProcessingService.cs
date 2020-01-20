using Optimation.Service.Abstractions;
using System.Threading.Tasks;

namespace Optimation.Service
{
    public class EmailTagProcessingService : IEmailTagProcessingService
    {
        /// <summary>
        /// Extracts data from element tags within a block of text
        /// </summary>
        /// <param name="text">Takes a block of text</param>
        /// <returns></returns>
        public Task ExtractData(string text)
        {
            throw new System.NotImplementedException();
        }
    }
}
