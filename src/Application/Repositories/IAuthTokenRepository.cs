using Connectify.src.Application.DTOs.DB.Responses;
using Connectify.src.Domain;

namespace Connectify.src.Application.Repositories
{
    public interface IAuthTokenRepository
    {
        Task<int?> GetUserIdByTokenValue(string value);
        Task<GetAuthTokenResponse?> GetByTokenValue(string value);
        Task Upsert(AuthToken authToken);
        Task Delete(int userId, string deviceId);
    }
}