using Connectify.src.Application.Base;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Connectify.src.Application.Attributes
{
    public class ValidateRequest<R, V> : ActionFilterAttribute where R : class where V : IValidator<R>, new()
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = (context.ActionArguments.Values.First(x => x is R) as R)!;
            var result = new V().Validate(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => new ValidationError(x.ErrorCode, x.ErrorMessage));
                context.Result = new BadRequestObjectResult(errors);
            }

            base.OnActionExecuting(context);
        }
    }
}