using EventBus.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sales.API.Interfaces;
using Sales.Domain.Models;
using System.Text.Json;
using static MassTransit.ValidationResultExtensions;

namespace Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IPlatesHandler _platesHandler;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger _logger;

        public SalesController(IPlatesHandler platesHandler, IPublishEndpoint publishEndpoint, ILogger<SalesController> logger)
        {
            _platesHandler = platesHandler;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        [HttpGet]
        [Route("getplates")]
        public async Task<ActionResult<IEnumerable<Plate>>> GetPlates(int pageNumber, int pageSize)
        {
            var results = _platesHandler.GetUnreservedPlates(pageNumber, pageSize);

            return Ok(JsonSerializer.Serialize(results));
        }

        [HttpGet]
        [Route("getsoldplates")]
        public async Task<ActionResult<IEnumerable<Plate>>> GetSoldPlates(int pageNumber, int pageSize)
        {
            var results = _platesHandler.GetSoldPlates(pageNumber, pageSize);

            return Ok(JsonSerializer.Serialize(results));
        }

        [HttpGet]
        [Route("getplatecount")]
        public  ActionResult<int> GetPlateCount(string filter)
        {
            var result = _platesHandler.GetPlateCount(filter);

            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpGet]
        [Route("getsoldplatecount")]
        public ActionResult<int> GetSoldPlateCount()
        {
            var result = _platesHandler.GetSoldPlateCount();

            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpGet]
        [Route("sellplate")]
        public async Task<ActionResult<Plate>> SellPlate(string registration)
        {
            var _plate = await _platesHandler.SellPlate(registration);
            if (_plate != null)
            {
                await _publishEndpoint.Publish(new PlateSoldEvent
                {
                    Id = _plate.Id,
                });

                _logger.LogInformation($"Plate sold with registration {registration} at {DateTime.Now}");
                _logger.LogInformation($"Event raised to update Commercial and Marketing db's with sold plate, registration {registration}");

                return Ok(JsonSerializer.Serialize(_plate));
            }
            return BadRequest();


            return Ok(JsonSerializer.Serialize(_plate));
        }

    }
}
