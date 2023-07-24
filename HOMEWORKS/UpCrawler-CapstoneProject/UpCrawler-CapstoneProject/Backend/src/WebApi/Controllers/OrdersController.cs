using Application.Features.Orders.Commands.Add;
using Application.Features.Orders.Commands.HardDelete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class OrdersController : ApiControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(OrderAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync(OrderUpdateCommand query)
        {
            return Ok(await Mediator.Send(query));
        }


        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAllAsync(OrderGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> HardDeleteAsync(OrderHardDeleteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
