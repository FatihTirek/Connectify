using Connectify.src.Application.Base;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record UpdateCommentRequest : CookieRequest
    {
        [FromRoute]
        public required int CommentId { get; set; }
        [FromBody]
        public required string Content { get; set; }
    }

    public class UpdateCommentValidator : AbstractValidator<UpdateCommentRequest>
    {
        public UpdateCommentValidator()
        {
            RuleFor(x => x.CommentId).NotEmpty();
            RuleFor(x => x.Content).NotEmpty().MaximumLength(2200);
        }
    }
}