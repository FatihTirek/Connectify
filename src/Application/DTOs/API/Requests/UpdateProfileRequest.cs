using Connectify.src.Application.Base;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record UpdateProfileRequest : CookieRequest
    {
        [FromBody]
        public required string UserName { get; set; }
        [FromForm]
        public IFormFile? ProfilePhoto { get; set; }
        [FromBody]
        public required int Day { get; set; }
        [FromBody]
        public required int Month { get; set; }
        [FromBody]
        public required int Year { get; set; }
    }

    public class UpdateProfileValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(3).MaximumLength(32);
            RuleFor(x => x.Day).NotEmpty().InclusiveBetween(1, 31);
            RuleFor(x => x.Month).NotEmpty().InclusiveBetween(1, 12);
            RuleFor(x => x.Year).NotEmpty().GreaterThanOrEqualTo(DateTime.UtcNow.Year - 100);
        }
    }
}