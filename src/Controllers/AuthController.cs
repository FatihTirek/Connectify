using Connectify.src.Application.Attributes;
using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;
using Connectify.src.Application.Helpers;
using Connectify.src.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [ValidateRequest<SignAttemptRequest, SignAttemptValidator>]
        [HttpPost("sign-attempt")]
        public async Task<IActionResult> SignAttempt([FromBody] SignAttemptRequest request)
        {
            var response = await authService.SignAttempt(request);
            if (response.GetType() == typeof(SignAttemptResponse.Failure)) return BadRequest(response);

            CookieHelper.SetAuthToken(Response, ((SignAttemptResponse.Success)response).AuthToken);

            return Created();
        }

        [ValidateRequest<LogAttemptRequest, LogAttemptValidator>]
        [HttpPost("log-attempt")]
        public async Task<IActionResult> LogAttempt([FromBody] LogAttemptRequest request)
        {
            var response = await authService.LogAttempt(request);
            if (response.GetType() == typeof(LogAttemptResponse.Failure)) return BadRequest(response);

            CookieHelper.SetAuthToken(Response, ((LogAttemptResponse.Success)response).AuthToken);

            return Ok(response);
        }

        [HttpPost("log-out")]
        public async Task<IActionResult> LogOut(LogOutRequest request)
        {
            var authToken = Request.Cookies["auth_token"];
            if (string.IsNullOrEmpty(authToken)) return Unauthorized();

            request.AuthToken = authToken;
            
            var response = await authService.LogOut(request);
            if (response.GetType() == typeof(LogOutResponse.Failure)) return BadRequest(response);

            Response.Cookies.Delete("auth_token");

            return NoContent();
        }

        [HttpGet("refresh-auth-token")]
        public async Task<IActionResult> RefreshAuthToken()
        { 
            var authToken = Request.Cookies["auth_token"];
            if (string.IsNullOrEmpty(authToken)) return Unauthorized();

            var response = await authService.RefreshAuthToken(new RefreshAuthTokenRequest() { AuthToken = authToken });
            if (response.GetType() == typeof(RefreshAuthTokenResponse.Failure)) return Unauthorized();
            
            CookieHelper.SetAuthToken(Response, ((RefreshAuthTokenResponse.Success)response).AuthToken);

            return NoContent();
        }
    }
}