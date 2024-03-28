using Connectify.src.Application.Repositories;
using Connectify.src.Domain;
using Microsoft.EntityFrameworkCore;

namespace Connectify.src.Infrastructure.Persistence.Repositories
{
    public class FriendshipRepository(ProjectContext context) : IFriendshipRepository
    {
        public async Task<bool> HasFollowing(int followerId, int followeeId)
        {
            return await context.Friendships.AnyAsync(x => x.FollowerId == followerId && x.FolloweeId == followeeId);
        }

        public async Task Create(Friendship friendship)
        {
            await context.Friendships.AddAsync(friendship);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int followerId, int followeeId)
        {
            await context.Friendships.Where(x => x.FollowerId == followerId && x.FolloweeId == followeeId).ExecuteDeleteAsync();
        }
    }
}