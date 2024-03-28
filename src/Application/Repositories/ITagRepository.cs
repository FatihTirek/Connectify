using Connectify.src.Domain;

namespace Connectify.src.Application.Repositories
{
    public interface ITagRepository
    {
        Task<List<Tag>> Search(string query); 
        Task<Tag?> GetById(int id); 
        Task<List<Tag>> GetByNames(List<string> name); 
        Task Create(Tag tag);
    }
}