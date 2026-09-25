using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_REST_CodeFirst.Models.DataManager
{
    public class UserManager : IDataRepository<User>
    {
        private readonly CinemaContext _context;

        public UserManager()
        {
        }

        public UserManager(CinemaContext context)
        {
            _context = context;
        }

        public ActionResult<IEnumerable<User>> GetAll()
        {
            return _context.Users.ToList();
        }

        public ActionResult<User> GetById(int id)
        {
            return _context.Users
                .FirstOrDefault(u => u.UserId == id);
        }

        public async Task<ActionResult<User>> GetByStringAsync(string mail)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Mail.ToUpper() == mail.ToUpper());
        }

        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User userToUpdate, User entity)
        {
            _context.Entry(userToUpdate).State = EntityState.Modified;

            userToUpdate.UserId = entity.UserId;
            userToUpdate.Name = entity.Name;
            userToUpdate.FirstName = entity.FirstName;
            userToUpdate.Mail = entity.Mail;
            userToUpdate.Street = entity.Street;
            userToUpdate.Postcode = entity.Postcode;
            userToUpdate.City = entity.City;
            userToUpdate.Country = entity.Country;
            userToUpdate.Latitude = entity.Latitude;
            userToUpdate.Longitude = entity.Longitude;
            userToUpdate.Pwd = entity.Pwd;
            userToUpdate.Mobile = entity.Mobile;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}