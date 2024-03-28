using Connectify.src.Application.Base;
using Connectify.src.Application.DTOs.DB.Responses;
using Connectify.src.Domain;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record GetTagPostsResponse
    {
        public record Success : GetTagPostsResponse
        {
            public IEnumerable<object> Posts { get; }

            public Success(IEnumerable<GetPostsResponse> Posts) => this.Posts = Posts.Select(x => new
            {
                x.Post.Id,
                x.Post.UserId,
                x.Post.Content,
                x.Post.LikeCount,
                x.Post.CommentCount,
                x.Post.MediaURLs,
                x.Post.CreatedAt,
                x.Tags
            });
        }

        public record Failure(IEnumerable<ValidationError> Errors) : GetTagPostsResponse;
    }
}