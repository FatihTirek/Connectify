using Connectify.src.Application.Base;
using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;
using Connectify.src.Application.Helpers;
using Connectify.src.Application.Repositories;
using Connectify.src.Application.Services;
using Connectify.src.Domain;
using Connectify.src.Infrastructure.Persistence.Errors;

namespace Connectify.src.Infrastructure.Persistence.Services
{
    public class AuthService(IUserRepository userRepository, IAuthTokenRepository authTokenRepository) : IAuthService
    {
        public async Task<SignAttemptResponse> SignAttempt(SignAttemptRequest request)
        {
            var birthDay = new DateOnly(request.Year, request.Month, request.Day);
            var isAtLeastThirteen = birthDay.ToDateTime(TimeOnly.MinValue).AddYears(13) < DateTime.UtcNow.Date;
            var isEmailExists = await userRepository.CheckIfEmailExists(request.Email);
            var isUserNameExists = await userRepository.CheckIfUserNameExists(request.UserName);

            if (!isAtLeastThirteen || isEmailExists || isUserNameExists)
            {
                var errors = new List<ValidationError>();

                if (isEmailExists) errors.Add(new AccountError.EmailExists());
                if (isUserNameExists) errors.Add(new AccountError.UserNameExists());
                if (!isAtLeastThirteen) errors.Add(new AccountError.YoungerThanThirteen());

                return new SignAttemptResponse.Failure(errors);
            }

            var authToken = new AuthToken()
            {
                DeviceId = request.DeviceId,
                Value = TokenHelper.GenerateAuthToken(),
            };

            var user = new User()
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = SecretHelper.Hash(request.Password),
                BirthDay = birthDay,
                AuthTokens = [authToken],
            };

            await userRepository.Create(user);
            return new SignAttemptResponse.Success(authToken.Value);
        }

        public async Task<LogAttemptResponse> LogAttempt(LogAttemptRequest request)
        {
            var result = await userRepository.GetByEmail(request.Email);

            if (result == null || !SecretHelper.Verify(request.Password, result.PasswordHash))
                return new LogAttemptResponse.Failure([new AccountError.InvalidCredentials()]);

            var authToken = new AuthToken()
            {
                UserId = result.UserId,
                DeviceId = request.DeviceId,
                Value = TokenHelper.GenerateAuthToken(),
            };

            await authTokenRepository.Upsert(authToken);

            return new LogAttemptResponse.Success(authToken.Value, result.IsMailVerified);
        }

        public async Task<LogOutResponse> LogOut(LogOutRequest request)
        {
            var result = await authTokenRepository.GetUserIdByTokenValue(request.AuthToken!);
            if (result == null) return new LogOutResponse.Failure([]);

            await authTokenRepository.Delete(result.Value, request.DeviceId);
            return new LogOutResponse.Success();
        }

        public async Task<RefreshAuthTokenResponse> RefreshAuthToken(RefreshAuthTokenRequest request)
        {
            var result = await authTokenRepository.GetByTokenValue(request.AuthToken);
            if (result == null) return new RefreshAuthTokenResponse.Failure([]);

            var authToken = new AuthToken()
            {
                UserId = result.UserId,
                DeviceId = result.DeviceId,
                Value = TokenHelper.GenerateAuthToken(),
            };

            await authTokenRepository.Upsert(authToken);
            return new RefreshAuthTokenResponse.Success(authToken.Value);
        }
    }
}