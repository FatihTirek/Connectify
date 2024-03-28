using Connectify.src.Application.Base;
using FluentValidation;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record VerifyConfirmationCodeRequest : CookieRequest
    {
        public required string Email { get; set; }
        public required string Code { get; set; }
        public required string DeviceId { get; set; }
        public required bool FromSignAttempt { get; set; }
    }

    public class VerifyConfirmationCodeValidator : AbstractValidator<VerifyConfirmationCodeRequest>
    {
        public VerifyConfirmationCodeValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MinimumLength(5).MaximumLength(96);
            RuleFor(x => x.Code).NotEmpty().Length(6);
            RuleFor(x => x.DeviceId).NotEmpty().MinimumLength(36);
            RuleFor(x => x.FromSignAttempt).NotNull();
        }
    }
}