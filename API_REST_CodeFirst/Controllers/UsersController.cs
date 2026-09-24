using API_REST_CodeFirst.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_REST_CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CinemaContext _context;

        public UsersController(CinemaContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

  
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUtilisateurById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        [HttpGet("email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Mail.ToUpper() == email.ToUpper());

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}