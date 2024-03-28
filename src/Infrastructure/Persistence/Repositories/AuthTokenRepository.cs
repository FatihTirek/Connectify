using Connectify.src.Application.DTOs.DB.Responses;
using Connectify.src.Application.Repositories;
using Connectify.src.Domain;
using Microsoft.EntityFrameworkCore;

namespace Connectify.src.Infrastructure.Persistence.Repositories
{
    public class AuthTokenRepository(ProjectContext context) : IAuthTokenRepository
    {
        public async Task<GetAuthTokenResponse?> GetByTokenValue(string value)
        {
            return await context.AuthTokens.Where(x => x.Value == value).Select(x => new GetAuthTokenResponse(x.UserId, x.DeviceId, x.User.IsMailVerified, x.ExpireAt)).FirstOrDefaultAsync();
        }

        public async Task<int?> GetUserIdByTokenValue(string value)
        {
            return await context.AuthTokens.Where(x => x.Value == value).Select(x => x.UserId).FirstOrDefaultAsync();
        }

        public async Task Upsert(AuthToken authToken)
        {
            var result = await context.AuthTokens.Where(x => x.UserId == authToken.UserId && x.DeviceId == authToken.DeviceId).FirstOrDefaultAsync();

            if (result == null)
            {
                await context.AuthTokens.AddAsync(authToken);
            }
            else
            {
                result.Value = authToken.Value;
                result.ExpireAt = authToken.ExpireAt;
            }

            await context.SaveChangesAsync();
        }

        public async Task Delete(int userId, string deviceId)
        {
            await context.AuthTokens.Where(x => x.UserId == userId && x.DeviceId == deviceId).ExecuteDeleteAsync();
        }
    }
}
