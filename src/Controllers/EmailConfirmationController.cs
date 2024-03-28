using Connectify.src.Application.Attributes;
using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;
using Connectify.src.Application.Helpers;
using Connectify.src.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Controllers
{
    [ApiController]
    [Route("api/email-confirmation")]
    public class EmailConfirmationController(IEmailConfirmationService emailConfirmationService) : ControllerBase
    {
        [ValidateRequest<SendConfirmationCodeRequest, SendConfirmationCodeValidator>(Order = 2)]
        [TypeFilter(typeof(ValidateUser<SendConfirmationCodeRequest>), Order = 1, Arguments = [false])]
        [HttpPost("send-confirmation-code")]
        public async Task<IActionResult> SendConfirmationCode([FromBody] SendConfirmationCodeRequest request)
        {
            var response = await emailConfirmationService.SendConfirmationCode(request);
            if (response.GetType() == typeof(SendConfirmationCodeResponse.Failure)) return BadRequest(response);
            return NoContent();
        }

        [ValidateRequest<VerifyConfirmationCodeRequest, VerifyConfirmationCodeValidator>(Order = 2)]
        [TypeFilter(typeof(ValidateUser<VerifyConfirmationCodeRequest>), Order = 1, Arguments = [false])]
        [HttpPost("verify-confirmation-code")]
        public async Task<IActionResult> VerifyConfirmationCode([FromBody] VerifyConfirmationCodeRequest request)
        {
            var response = await emailConfirmationService.VerifyConfirmationCode(request);
            if (response.GetType() == typeof(VerifyConfirmationCodeResponse.Failure)) return BadRequest(response);

            CookieHelper.SetAuthToken(Response, ((VerifyConfirmationCodeResponse.Success)response).AuthToken);

            return NoContent();
        }
    }
}
