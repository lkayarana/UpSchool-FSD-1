using Application.Features.Products.Commands.Add;
using Application.Features.Products.Commands.HardDelete;
using Application.Features.Products.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(ProductAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAllAsync(ProductGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        //[HttpPost("Delete")]
        //public async Task<IActionResult> DeleteAsync(ProductDeleteCommand command)
        //{
        //    return Ok(await Mediator.Send(command));
        //}

        //[HttpPost("Update")]
        //public async Task<IActionResult> UpdateAsync(ProductUpdateCommand command)
        //{
        //    return Ok(await Mediator.Send(command));
        //}

        [HttpDelete("Delete")]
        public async Task<IActionResult> HardDeleteAsync(ProductHardDeleteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }

}
