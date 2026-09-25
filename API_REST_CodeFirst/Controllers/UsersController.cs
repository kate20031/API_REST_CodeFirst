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


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUtilisateurById),
                new { id = user.UserId },
                user);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            if (id != user.UserId)
            {
                return BadRequest();
            }

            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                
                return NotFound();
            }

            existingUser.Name = user.Name;
            existingUser.FirstName = user.FirstName;
            existingUser.Mobile = user.Mobile;
            existingUser.Mail = user.Mail;
            existingUser.Pwd = user.Pwd;
            existingUser.Street = user.Street;
            existingUser.Postcode = user.Postcode;
            existingUser.City = user.City;
            existingUser.Country = user.Country;
            existingUser.Latitude = user.Latitude;
            existingUser.Longitude = user.Longitude;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}