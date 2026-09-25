using API_REST_CodeFirst.Models.EntityFramework;

namespace API_REST_CodeFirst.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        Task<User> Add(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}