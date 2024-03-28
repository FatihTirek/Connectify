using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;
using Connectify.src.Application.Repositories;
using Connectify.src.Application.Services;

namespace Connectify.Infrastructure.Persistence.Services
{
    public class SearchService(IUserRepository userRepository, ITagRepository tagRepository) : ISearchService
    {
        public async Task<SearchResponse> Search(SearchRequest request)
        {
            var users = await userRepository.Search(request.Query);
            var tags = await tagRepository.Search(request.Query);
           
            return new SearchResponse.Success(users, tags);
        }
    }
}