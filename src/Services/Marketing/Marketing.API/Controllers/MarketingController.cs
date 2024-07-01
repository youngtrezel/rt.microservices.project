using EventBus.Events;
using Marketing.API.Interfaces;
using Marketing.Domain.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Marketing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketingController : ControllerBase
    {
        private readonly IPlatesHandler _platesHandler;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger _logger;

        public MarketingController(IPlatesHandler platesHandler, IPublishEndpoint publishEndpoint, ILogger<MarketingController> logger)
        {
            _platesHandler = platesHandler;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        [HttpGet]
        [Route("getplates")]
        public async Task<ActionResult<IEnumerable<Plate>>> GetPlates(int pageNumber, int pageSize, bool ascending)
        {
            var results = await _platesHandler.GetPaginationPlates(pageNumber, pageSize, ascending);

            return Ok(JsonSerializer.Serialize(results));
        }

        [HttpGet]
        [Route("getfilteredplates")]
        public async Task<ActionResult<IEnumerable<Plate>>> GetFilteredPlates(string letters, int pageNumber, int pageSize, bool ascending)
        {
            var results = await _platesHandler.GetFilteredPlates(letters, pageNumber, pageSize, ascending);

            return Ok(JsonSerializer.Serialize(results));
        }

        [HttpGet]
        [Route("filteredplatecount")]
        public ActionResult<int> GetFilteredPlateCount(string letters)
        {
            var results = _platesHandler.GetFilteredPlatesCount(letters);

            return Ok(JsonSerializer.Serialize(results));
        }


    }
}
