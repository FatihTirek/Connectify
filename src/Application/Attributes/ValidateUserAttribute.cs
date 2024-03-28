using Connectify.src.Application.Base;
using Connectify.src.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Connectify.src.Application.Attributes
{
    public class ValidateUser<R>(IAuthTokenRepository authTokenRepository, bool Verify = true) : ActionFilterAttribute where R : CookieRequest
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authToken = context.HttpContext.Request.Cookies["auth_token"];

            if (string.IsNullOrEmpty(authToken))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var result = await authTokenRepository.GetByTokenValue(authToken);

            if (result == null || result.ExpireAt < DateTime.UtcNow || (Verify && !result.IsMailVerified))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            (context.ActionArguments.Values.First(x => x is R) as R)!.UserId = result.UserId;

            await next();
        }
    }
}