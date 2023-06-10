using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using UpSchool.Domain.Dtos;
using UpSchool.WebApi.Hubs;

namespace UpSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeleniumLogController : Controller
    {
        private readonly IHubContext<SeleniumLogHub> _seleniumHubContext;

        public SeleniumLogController(IHubContext<SeleniumLogHub> seleniumHubContext)
        {
            _seleniumHubContext = seleniumHubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendLogNotificationAsync(SendLogNotificationApiDto logNotificationApiDto)
        {
            await _seleniumHubContext.Clients.AllExcept(logNotificationApiDto.ConnectionId)
                .SendAsync("NewSeleniumLogAdded", logNotificationApiDto.Log);

            return Ok();
        }
    }
}
