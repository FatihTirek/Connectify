using Connectify.src.Application.DTOs.DB.Responses;
using Connectify.src.Application.Repositories;
using Connectify.src.Domain;
using Connectify.src.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Connectify.Infrastructure.Persistence.Repositories
{
    public class PostRepository(ProjectContext context) : IPostRepository
    {
        private const int MaxPostFetchSize = 15;

        public async Task<List<GetPostsResponse>> GetRecentPosts(int? highestId, int? lowestId)
        {
            if (highestId == null)
            {
                return await context.Posts
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(MaxPostFetchSize)
                    .Select(x => new GetPostsResponse(x, x.Tags.Select(x => x.Name).ToList()))
                    .ToListAsync();
            }

            var result = await context.Posts
                .Where(x => x.Id > highestId)
                .OrderBy(x => x.CreatedAt)
                .Take(MaxPostFetchSize)
                .Select(x => new GetPostsResponse(x, x.Tags.Select(x => x.Name).ToList()))
                .ToListAsync();
            result.Reverse();

            if (result.Count < MaxPostFetchSize)
            {
                var other = await context.Posts
                    .Where(x => x.Id < lowestId)
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(MaxPostFetchSize - result.Count)
                    .Select(x => new GetPostsResponse(x, x.Tags.Select(x => x.Name).ToList()))
                    .ToListAsync();
                result.AddRange(other);
            }

            return result;
        }

        public async Task<List<GetPostsResponse>> GetTagPosts(string tagName, int? lowestId)
        {
            if (lowestId == null) return await context.Posts.Where(x => x.Tags.Any(x => x.Name == tagName)).OrderByDescending(x => x.Id).Take(MaxPostFetchSize).Select(x => new GetPostsResponse(x, x.Tags.Select(x => x.Name).ToList())).ToListAsync();
            return await context.Posts.Where(x => x.Tags.Any(x => x.Name == tagName) && x.Id < lowestId).OrderByDescending(x => x.Id).Take(MaxPostFetchSize).Select(x => new GetPostsResponse(x, x.Tags.Select(x => x.Name).ToList())).ToListAsync();
        }

        public Task<List<string>> GetMediaURLsById(int postId)
        {
            return context.Posts.Where(x => x.Id == postId).Select(x => x.MediaURLs).FirstAsync();
        }

        public async Task Create(Post post)
        {
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
        }

        public async Task UpdateContent(int id, int userId, string content)
        {
            await context.Posts.Where(x => x.Id == id && x.UserId == userId).ExecuteUpdateAsync(x => x.SetProperty(x => x.Content, content));
        }

        public async Task Delete(int id, int userId)
        {
            await context.Posts.Where(x => x.Id == id && x.UserId == userId).ExecuteDeleteAsync();
        }
    }
}