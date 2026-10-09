using API_REST_CodeFirst.Models.DataManager;
using API_REST_CodeFirst.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_CodeFirst.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeriesController : ControllerBase
    {
        private readonly ISerieManager _serieManager;

        public SeriesController(ISerieManager serieManager)
        {
            _serieManager = serieManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SerieDto>> GetSeries()
        {
            return Ok(_serieManager.GetSeries());
        }

        [HttpGet("{id}")]
        public ActionResult<SerieDetailDto> GetSerie(int id)
        {
            var serie = _serieManager.GetSerieById(id);

            if (serie == null)
                return NotFound();

            return Ok(serie);
        }

        [HttpGet("Networks")]
        public ActionResult<IEnumerable<NetworkDto>> GetNetworks()
        {
            return Ok(_serieManager.GetNetworks());
        }
    }
}