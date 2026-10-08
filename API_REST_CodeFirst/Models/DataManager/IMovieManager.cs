using API_REST_CodeFirst.Models.DTO;

namespace API_REST_CodeFirst.Models.DataManager
{
    public interface IMovieManager
    {
        IEnumerable<MovieDto> GetAll();
        MovieDetailDto? GetById(int id);
    }
}