using System.Threading.Tasks;

namespace Optimation.Service.Abstractions
{
    public interface IEmailTagProcessingService
    {
        Task ExtractData(string text);
    }
}
