using Application.Features.SendEmail;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ApiControllerBase
    {
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmailAsync(SendEmailAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
