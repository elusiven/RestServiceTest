using Optimation.Service.Primitives.Models;
using System.Threading.Tasks;

namespace Optimation.Service.Abstractions
{
    public interface IEmailProcessingService
    {
        Task<ReservationResourceModel> ExtractReservation(string text);
        Task<ExpenseResourceModel> ExtractExpense(string text);
    }
}
