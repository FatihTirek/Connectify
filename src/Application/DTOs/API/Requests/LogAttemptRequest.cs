using FluentValidation;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record LogAttemptRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string DeviceId { get; set; }
    }

    public class LogAttemptValidator : AbstractValidator<LogAttemptRequest>
    {
        public LogAttemptValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MinimumLength(5).MaximumLength(96);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(64);
        }
    }
}