using FluentValidation;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record SignAttemptRequest
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string DeviceId { get; set; }
        public required int Day { get; set; }
        public required int Month { get; set; }
        public required int Year { get; set; }
    }

    public class SignAttemptValidator : AbstractValidator<SignAttemptRequest>
    {
        public SignAttemptValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(3).MaximumLength(32);
            RuleFor(x => x.Email).NotEmpty().MinimumLength(5).MaximumLength(96);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(64);
            RuleFor(x => x.DeviceId).NotEmpty().MinimumLength(36);
            RuleFor(x => x.Day).NotEmpty().InclusiveBetween(1, 31);
            RuleFor(x => x.Month).NotEmpty().InclusiveBetween(1, 12);
            RuleFor(x => x.Year).NotEmpty().GreaterThanOrEqualTo(DateTime.UtcNow.Year - 100);
        }
    }
}