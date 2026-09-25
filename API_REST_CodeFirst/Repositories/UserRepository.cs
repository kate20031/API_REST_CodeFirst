using Microsoft.EntityFrameworkCore;
using API_REST_CodeFirst.Models.EntityFramework;

namespace API_REST_CodeFirst.Repositories
{
    public class UserRepository : IUserRepository

    {
        private readonly CinemaContext _context;

        public UserRepository(CinemaContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }


        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Mail.ToUpper() == email.ToUpper());
        }


        public async Task<User> Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}