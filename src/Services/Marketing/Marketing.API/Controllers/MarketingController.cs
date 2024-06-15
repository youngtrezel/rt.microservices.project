using Marketing.API.Interfaces;
using Marketing.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Marketing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketingController : ControllerBase
    {
        private readonly IPlatesHandler _platesHandler;

        public MarketingController(IPlatesHandler platesHandler)
        {
            _platesHandler = platesHandler;
        }

        [HttpGet]
        [Route("getplates")]
        public ActionResult<IEnumerable<Plate>> GetPlates(int pageNumber, int pageSize)
        {
            var results = _platesHandler.GetPaginationPlates(pageNumber, pageSize);

            return Ok(JsonSerializer.Serialize(results));
        }

        [HttpGet]
        [Route("getunreservedplates")]
        public ActionResult<IEnumerable<Plate>> GetFilteredPlates(string letters)
        {
            var results = _platesHandler.GetFilteredPlates(letters);

            return Ok(JsonSerializer.Serialize(results));
        }
    }
}
