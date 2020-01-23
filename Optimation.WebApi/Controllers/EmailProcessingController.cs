using Microsoft.AspNetCore.Mvc;
using Optimation.Service.Abstractions;
using Optimation.Service.Primitives.Models;
using System.Threading.Tasks;

namespace Optimation.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class EmailProcessingController : Controller
    {
        private readonly IEmailProcessingService _emailTagProcessingService;

        public EmailProcessingController(IEmailProcessingService emailTagProcessingService)
        {
            _emailTagProcessingService = emailTagProcessingService;
        }   

        [HttpPost("Extract/Expense"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ExpenseResourceModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> Expense1_0([FromBody] string text)
        {
            return Ok(await _emailTagProcessingService.ExtractExpenseAsync(text));
        }

        [HttpPost("Extract/Reservation"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ReservationResourceModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> Reservation1_0([FromBody] string text)
        {
            return Ok(await _emailTagProcessingService.ExtractReservationAsync(text));
        }
    }
}
