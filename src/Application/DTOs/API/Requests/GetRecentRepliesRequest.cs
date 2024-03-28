using Connectify.src.Application.Base;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record GetRecentRepliesRequest : CookieRequest
    {
        [FromRoute]
        public required int RepliedCommentId { get; set; }
        [FromQuery]
        public int? LowestId { get; set; }
    }
}