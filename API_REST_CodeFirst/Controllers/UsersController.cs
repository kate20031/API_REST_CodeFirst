using API_REST_CodeFirst.Models.DataManager;
using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDataRepository<User> _dataRepository;

        public UsersController(IDataRepository<User> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _dataRepository.GetAll();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUtilisateurById(int id)
        {
            var user = await _dataRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var user = await _dataRepository.GetByStringAsync(email);

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

            await _dataRepository.AddAsync(user);

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

            var existingUser = await _dataRepository.GetById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            await _dataRepository.UpdateAsync(existingUser, user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _dataRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            await _dataRepository.DeleteAsync(user);

            return NoContent();
        }
    }
}