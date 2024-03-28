using Connectify.src.Application.DTOs.DB.Responses;
using Connectify.src.Application.Repositories;
using Connectify.src.Domain;
using Microsoft.EntityFrameworkCore;

namespace Connectify.src.Infrastructure.Persistence.Repositories
{
    public class UserRepository(ProjectContext context) : IUserRepository
    {
        public async Task<List<GetUsersBySearchResponse>> Search(string query)
        {
            var config = "english";
            return await context.Users.Where(x => EF.Functions.ToTsVector(config, x.UserName).Matches(EF.Functions.ToTsQuery(config, query)))
                .OrderByDescending(x => EF.Functions.ToTsVector(config, x.UserName).Rank(EF.Functions.ToTsQuery(config, query)))
                .Select(x => new GetUsersBySearchResponse(x.Id, x.UserName, x.ProfilePhotoURL))
                .ToListAsync();
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            return await context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> CheckIfUserNameExists(string userName, int? userId = null)
        {
            return await context.Users.AnyAsync(x => x.UserName == userName && x.Id != userId);
        }

        public async Task<GetUserByEmailResponse?> GetByEmail(string email)
        {
            return await context.Users.Where(x => x.Email == email).Select(x => new GetUserByEmailResponse(x.Id, x.PasswordHash, x.IsMailVerified)).FirstOrDefaultAsync();
        }

        public async Task<int> Create(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task UpdateEmail(int userId, string email)
        {
            await context.Users.Where(x => x.Id == userId).ExecuteUpdateAsync(x => x.SetProperty(x => x.Email, email));
        }

        // public async Task UpdateProfile(int userId, string userName, DateTime birthDay, string? profilePhotoURL = null)
        // {
        //     await context.Users.Where(x => x.Id == userId)
        //         .ExecuteUpdateAsync(x => x.SetProperty(x => x.UserName, userName)
        //             .SetProperty(x => x.ProfilePhotoURL, profilePhotoURL)
        //             .SetProperty(x => x.BirthDay, birthDay));
        // }

        public async Task MarkEmailAsVerified(int userId)
        {
            await context.Users.Where(x => x.Id == userId).ExecuteUpdateAsync(x => x.SetProperty(x => x.IsMailVerified, true));
        }
    }
}