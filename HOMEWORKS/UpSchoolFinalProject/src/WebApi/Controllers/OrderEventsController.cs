using Application.Features.OrderEvents.Commands.Add;
using Application.Features.OrderEvents.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class OrderEventsController : ApiControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(OrderEventAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync(OrderEventGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
