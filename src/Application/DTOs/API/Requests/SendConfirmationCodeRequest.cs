using Connectify.src.Application.Base;
using FluentValidation;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record SendConfirmationCodeRequest : CookieRequest
    {
        public required string Email { get; set; }
    }

    public class SendConfirmationCodeValidator : AbstractValidator<SendConfirmationCodeRequest>
    {
        public SendConfirmationCodeValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MinimumLength(5).MaximumLength(96);
        }
    }
}