using Connectify.src.Application.Repositories;
using Connectify.src.Domain;
using Microsoft.EntityFrameworkCore;

namespace Connectify.src.Infrastructure.Persistence.Repositories
{
    public class TagRepository(ProjectContext context) : ITagRepository
    {
        public async Task<List<Tag>> Search(string query)
        {
            var config = "english";
            return await context.Tags.Where(x => EF.Functions.ToTsVector(config, x.Name).Matches(EF.Functions.ToTsQuery(config, query)))
                .OrderByDescending(x => EF.Functions.ToTsVector(config, x.Name).Rank(EF.Functions.ToTsQuery(config, query)))
                .ToListAsync();
        }

        public async Task<Tag?> GetById(int id)
        {
            return await context.Tags.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Tag>> GetByNames(List<string> names)
        {
            return await context.Tags.Where(x => names.Contains(x.Name)).ToListAsync();
        }

        public async Task Create(Tag tag)
        {
            await context.Tags.AddAsync(tag);
            await context.SaveChangesAsync();
        }
    }
}