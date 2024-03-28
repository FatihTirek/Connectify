using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;

namespace Connectify.src.Application.Services
{
    public interface IFriendShipService
    {
        Task<FollowUserResponse> FollowUser(FollowUserRequest request);
        Task<UnfollowUserResponse> UnfollowUser(UnfollowUserRequest request);
    }
}