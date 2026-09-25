using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace API_REST_CodeFirst.Models.DataManager
{
    public class UserManager : IDataRepository<User>
    {
        private readonly CinemaContext _context;

        public UserManager(CinemaContext context)
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

        public async Task<User?> GetByStringAsync(string mail)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Mail.ToUpper() == mail.ToUpper());
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User userToUpdate, User user)
        {
            userToUpdate.Name = user.Name;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.Mail = user.Mail;
            userToUpdate.Street = user.Street;
            userToUpdate.Postcode = user.Postcode;
            userToUpdate.City = user.City;
            userToUpdate.Country = user.Country;
            userToUpdate.Latitude = user.Latitude;
            userToUpdate.Longitude = user.Longitude;
            userToUpdate.Pwd = user.Pwd;
            userToUpdate.Mobile = user.Mobile;

            _context.Entry(userToUpdate).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}