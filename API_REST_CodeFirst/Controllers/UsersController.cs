using API_REST_CodeFirst.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using API_REST_CodeFirst.Repositories;

namespace API_REST_CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _repository.GetAll();

            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUtilisateurById(int id)
        {
            var user = await _repository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        [HttpGet("email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var user = await _repository.GetByEmail(email);

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

            user = await _repository.Add(user);

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

            var existingUser = await _repository.GetById(id);

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

            await _repository.Update(existingUser);

            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            await _repository.Delete(id);

            return NoContent();
        }

    }
}