using Connectify.src.Application.Base;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record UnfollowUserRequest : CookieRequest
    {
        [FromRoute]
        public required int FolloweeId { get; set; }
    }
}