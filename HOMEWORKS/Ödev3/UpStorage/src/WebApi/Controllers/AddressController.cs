using Application.Features.Addresses.Commands.Add;
using Application.Features.Addresses.Commands.Delete;
using Application.Features.Addresses.Commands.HardDelete;
using Application.Features.Addresses.Commands.Update;
using Application.Features.Addresses.Queries.GetAll;
using Application.Features.Addresses.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class AddressesController : ApiControllerBase
    {
        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(AddressAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetByIdAsync(AddressGetByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAllAsync(AddressGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(AddressUpdateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(AddressDeleteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(AddressHardDeleteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
