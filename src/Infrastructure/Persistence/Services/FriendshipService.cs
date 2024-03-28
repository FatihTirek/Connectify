using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;
using Connectify.src.Application.Repositories;
using Connectify.src.Application.Services;
using Connectify.src.Domain;

namespace Connectify.Infrastructure.Persistence.Services
{
    public class FriendshipService(IFriendshipRepository friendshipRepository) : IFriendShipService
    {
        public async Task<FollowUserResponse> FollowUser(FollowUserRequest request)
        {
            var result = await friendshipRepository.HasFollowing(request.UserId, request.FolloweeId);
            if (!result) await friendshipRepository.Create(new Friendship { FollowerId = request.UserId, FolloweeId = request.FolloweeId });
            return new FollowUserResponse.Success();
        }

        public async Task<UnfollowUserResponse> UnfollowUser(UnfollowUserRequest request)
        {
            var result = await friendshipRepository.HasFollowing(request.UserId, request.FolloweeId);
            if (result) await friendshipRepository.Delete(request.UserId, request.FolloweeId);
            return new UnfollowUserResponse.Success();
        }
    }
}