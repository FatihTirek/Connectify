using Connectify.src.Application.Base;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record CreatePostRequest : CookieRequest
    {
        [FromForm]
        public string? Content { get; set; }
        [FromForm]
        public List<string> Tags { get; set; } = [];
        [FromForm]
        public List<IFormFile> Medias { get; set; } = [];
    }

    public class CreatePostValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Content).MaximumLength(2200);
            RuleFor(x => x.Tags).Must(x => x.Count <= 30).WithErrorCode("TagCountExceeded").WithMessage("Tag count exceeded");
            RuleFor(x => x.Medias).Must(x => x.Count <= 10).WithErrorCode("MediaCountExceeded").WithMessage("Media count exceeded");
        }
    }
}