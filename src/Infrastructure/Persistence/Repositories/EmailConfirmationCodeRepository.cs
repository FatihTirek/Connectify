using Connectify.src.Application.Repositories;
using Connectify.src.Domain;
using Microsoft.EntityFrameworkCore;

namespace Connectify.src.Infrastructure.Persistence.Repositories
{
    public class EmailConfirmationCodeRepository(ProjectContext context) : IEmailConfirmationCodeRepository
    {
        public async Task<EmailConfirmationCode?> GetByEmail(string email)
        {
            return await context.EmailConfirmationCodes.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task Upsert(EmailConfirmationCode confirmationCode)
        {
            var result = await context.EmailConfirmationCodes.Where(x => x.UserId == confirmationCode.UserId).FirstOrDefaultAsync();

            if (result == null)
            {
                await context.EmailConfirmationCodes.AddAsync(confirmationCode);
            }
            else
            {
                result.Email = confirmationCode.Email;
                result.Value = confirmationCode.Value;
                result.ExpireAt = confirmationCode.ExpireAt;
            }

            await context.SaveChangesAsync();
        }

        public async Task Delete(string email)
        {
            await context.EmailConfirmationCodes.Where(x => x.Email == email).ExecuteDeleteAsync();
        }
    }
}
