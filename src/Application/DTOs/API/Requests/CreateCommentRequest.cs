using Connectify.src.Application.Base;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record CreateCommentRequest : CookieRequest
    {
        [FromQuery]
        public required int PostId { get; set; }
        [FromQuery]
        public int? RepliedCommentId { get; set; }
        [FromBody]
        public required string Content { get; set; }
    }

    public class CreateCommentValidator : AbstractValidator<CreateCommentRequest>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.Content).MaximumLength(2200);
        }
    }
}