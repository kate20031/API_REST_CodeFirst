using API_REST_CodeFirst.Models.DataManager;
using API_REST_CodeFirst.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieManager _movieManager;

        public MoviesController(MovieManager movieManager)
        {
            _movieManager = movieManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieDto>> GetMovies()
        {
            return Ok(_movieManager.GetAll());
        }
    }
}