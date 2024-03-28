using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;
using Connectify.src.Application.Helpers;
using Connectify.src.Application.Repositories;
using Connectify.src.Application.Services;
using Connectify.src.Domain;
using Connectify.src.Infrastructure.Persistence.Errors;

namespace Connectify.src.Infrastructure.Persistence.Services
{
    public class EmailConfirmationService(IEmailConfirmationCodeRepository EmailConfirmationCodeRepository, IUserRepository userRepository, IAuthTokenRepository authTokenRepository, ISendEmailService sendEmailService) : IEmailConfirmationService
    {
        public async Task<SendConfirmationCodeResponse> SendConfirmationCode(SendConfirmationCodeRequest request)
        {
            var confirmationCode = new EmailConfirmationCode
            {
                UserId = request.UserId,
                Email = request.Email,
                Value = SecretHelper.GenerateSixDigitCode(),
            };

            await EmailConfirmationCodeRepository.Upsert(confirmationCode);
            await sendEmailService.SendConfirmationCode(confirmationCode.Email, confirmationCode.Value);

            return new SendConfirmationCodeResponse.Success();
        }

        public async Task<VerifyConfirmationCodeResponse> VerifyConfirmationCode(VerifyConfirmationCodeRequest request)
        {
            var result = await EmailConfirmationCodeRepository.GetByEmail(request.Email);

            if (result == null)
                return new VerifyConfirmationCodeResponse.Failure([new EmailConfirmationError.CodeNotExists()]);
            if (result.Value != request.Code)
                return new VerifyConfirmationCodeResponse.Failure([new EmailConfirmationError.InvalidCode()]);
            if (result.ExpireAt < DateTime.UtcNow)
                return new VerifyConfirmationCodeResponse.Failure([new EmailConfirmationError.CodeExpired()]);

            await EmailConfirmationCodeRepository.Delete(result.Email);

            if (request.FromSignAttempt) await userRepository.MarkEmailAsVerified(result.UserId);
            else await userRepository.UpdateEmail(request.UserId, result.Email);

            var authToken = new AuthToken()
            {
                UserId = result.UserId,
                DeviceId = request.DeviceId,
                Value = TokenHelper.GenerateAuthToken(),
            };
            
            await authTokenRepository.Upsert(authToken);
            return new VerifyConfirmationCodeResponse.Success(authToken.Value);
        }
    }
}