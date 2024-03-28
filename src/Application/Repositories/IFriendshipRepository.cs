using Connectify.src.Domain;

namespace Connectify.src.Application.Repositories
{
    public interface IFriendshipRepository
    {
        Task<bool> HasFollowing(int followerId, int followeeId); 
        Task Create(Friendship friendship);
        Task Delete(int followerId, int followeeId);
    }
}