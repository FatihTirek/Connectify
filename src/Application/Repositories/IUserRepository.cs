using Connectify.src.Application.DTOs.DB.Responses;
using Connectify.src.Domain;

namespace Connectify.src.Application.Repositories
{
    public interface IUserRepository
    {
        Task<List<GetUsersBySearchResponse>> Search(string query); 
        Task<bool> CheckIfEmailExists(string email);
        Task<bool> CheckIfUserNameExists(string userName, int? userId = null);
        Task<GetUserByEmailResponse?> GetByEmail(string email);
        Task<int> Create(User user);
        Task UpdateEmail(int userId, string email);
        // Task UpdateProfile(int userId, string userName, DateTime birthDay, string? profilePhotoURL = null);
        Task MarkEmailAsVerified(int userId);
    }
}