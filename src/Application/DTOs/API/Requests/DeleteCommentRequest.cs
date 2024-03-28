using Connectify.src.Application.Base;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record DeleteCommentRequest : CookieRequest
    {
        [FromRoute]
        public required int CommentId { get; set; }
    }
}