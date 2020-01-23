using Optimation.Service.Primitives.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Optimation.Service.Abstractions
{
    public interface IEmailProcessingService
    {
        Task<ReservationResourceModel> ExtractReservationAsync(string text, CancellationToken cancellationToken = default);
        Task<ExpenseResourceModel> ExtractExpenseAsync(string text, CancellationToken cancellationToken = default);
    }
}
