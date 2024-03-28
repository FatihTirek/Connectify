using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;

namespace Connectify.src.Application.Services
{
    public interface IEmailConfirmationService
    {
        Task<SendConfirmationCodeResponse> SendConfirmationCode(SendConfirmationCodeRequest request);
        Task<VerifyConfirmationCodeResponse> VerifyConfirmationCode(VerifyConfirmationCodeRequest request);
    }
}