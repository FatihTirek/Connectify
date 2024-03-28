using System.Diagnostics.CodeAnalysis;
using Connectify.src.Application.Attributes;
using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;
using Connectify.src.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Controllers
{
    [ApiController]
    [Route("api/friendships")]
    public class FriendshipController(IFriendShipService friendShipService) : ControllerBase
    {
        [SuppressMessage("Usage", "ASP0018:Unused route parameter", Justification = "<Pending>")]
        [ServiceFilter(typeof(ValidateUser<FollowUserRequest>))]
        [HttpPost("follow/{FolloweeId}")]
        public async Task<IActionResult> FollowUser(FollowUserRequest request)
        {
            var response = await friendShipService.FollowUser(request);
            if (response.GetType() == typeof(FollowUserResponse.Failure)) return BadRequest(response);

            return NoContent();
        }

        [SuppressMessage("Usage", "ASP0018:Unused route parameter", Justification = "<Pending>")]
        [ServiceFilter(typeof(ValidateUser<UnfollowUserRequest>))]
        [HttpPost("unfollow/{FolloweeId}")]
        public async Task<IActionResult> UnfollowUser(UnfollowUserRequest request)
        {
            var response = await friendShipService.UnfollowUser(request);
            if (response.GetType() == typeof(UnfollowUserResponse.Failure)) return BadRequest(response);

            return NoContent();
        }
    }
}
