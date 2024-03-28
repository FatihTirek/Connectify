using Connectify.src.Application.Base;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record UpdatePostRequest : CookieRequest
    {
        [FromRoute]
        public required int PostId { get; set; }
        [FromBody]
        public required string Content { get; set; }
    }

    public class UpdatePostValidator : AbstractValidator<UpdatePostRequest>
    {
        public UpdatePostValidator()
        {
            RuleFor(x => x.PostId).NotEmpty();
            RuleFor(x => x.Content).NotEmpty().MaximumLength(2200);
        }
    }
}