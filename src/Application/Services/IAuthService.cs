using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;

namespace Connectify.src.Application.Services
{
    public interface IAuthService
    {
        Task<SignAttemptResponse> SignAttempt(SignAttemptRequest request);
        Task<LogAttemptResponse> LogAttempt(LogAttemptRequest request);
        Task<LogOutResponse> LogOut(LogOutRequest request);
        Task<RefreshAuthTokenResponse> RefreshAuthToken(RefreshAuthTokenRequest request);
    }
}