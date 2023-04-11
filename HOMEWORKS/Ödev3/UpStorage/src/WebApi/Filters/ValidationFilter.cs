using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
{
    public class ValidationFilter:ActionFilterAttribute
    {

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Select(x => x.Value.Errors)
                    .Where(y => y.Count() > 0)
                    .ToList();

                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
