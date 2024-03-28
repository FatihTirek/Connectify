using Connectify.src.Application.Base;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record UnlikePostRequest : CookieRequest
    {
        [FromRoute]
        public required int PostId { get; set; }
    }
}