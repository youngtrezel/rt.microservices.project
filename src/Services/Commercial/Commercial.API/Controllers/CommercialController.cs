using Commercial.API.Interfaces;
using Commercial.Domain.Models;
using Commercial.Domain.Models.Data;
using EventBus.Events;
using EventBus.Events.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Commercial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommercialController : ControllerBase
    {

        private readonly IPlatesHandler _platesHandler;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger _logger;

        public CommercialController(IPlatesHandler platesHandler, IPublishEndpoint publishEndpoint, ILogger<CommercialController> logger)
        {
            _platesHandler = platesHandler;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        [HttpGet]
        [Route("getplate")]
        public async Task<ActionResult<Plate>> GetPlate(string registration)
        {

            var result = await _platesHandler.GetPlate(registration);

            if(result.Id == Guid.Empty)
            {
                return BadRequest();
            }

            return Ok(JsonSerializer.Serialize(result));
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
        public ActionResult<IEnumerable<Plate>> GetUnreservedPlates(int pageNumber, int pageSize)
        {
            var results = _platesHandler.GetUnreservedPlates(pageNumber, pageSize);

            return Ok(JsonSerializer.Serialize(results));
        }

        [HttpGet]
        [Route("getunsoldplates")]
        public ActionResult<IEnumerable<Plate>> GetUnsoldPlates(int pageNumber, int pageSize)
        {
            var results = _platesHandler.GetUnsoldPlates(pageNumber, pageSize);

            return Ok(JsonSerializer.Serialize(results));
        }

        [HttpPost]
        [Route("addplate")]
        public async Task<ActionResult<Plate>> AddPlate(PlateDto plate)
        {
            var _plate = await _platesHandler.AddPlate(plate);
            if (_plate != null)
            {
                await _publishEndpoint.Publish(new PlateAddedEvent
                {
                    Id = _plate.Id,
                    SalePrice = _plate.SalePrice,
                    DateSold = _plate.DateSold,
                    Sold = _plate.Sold,
                    PriceSoldFor = _plate.PriceSoldFor,
                    PurchasePrice = _plate.PurchasePrice,
                    Letters = _plate.Letters,
                    Numbers = _plate.Numbers,
                    Reserved = _plate.Reserved,
                    Registration = _plate.Registration
                });

                return Ok(JsonSerializer.Serialize(plate));
            }
            return BadRequest();          
        }

        [HttpPut]
        [Route("reserveplate")]
        public async Task<ActionResult> ReservePlate(string registration)
        {
            var _plate = await _platesHandler.ReservePlate(registration);

            if( _plate.Reserved == true )
            {
                await _publishEndpoint.Publish(new PlateReservedEvent
                {
                    Id = _plate.Id
                }); ;

                return Ok(JsonSerializer.Serialize(_plate));
            }

            return StatusCode(501);
        }

        [HttpPut]
        [Route("unreserveplate")]
        public async Task<ActionResult> UnreservePlate(string registration)
        {
            var _plate = await _platesHandler.UnreservePlate(registration);

            if (_plate.Reserved == false)
            {
                await _publishEndpoint.Publish(new PlateUnreservedEvent
                {
                    Id = _plate.Id
                });
            
                return Ok(JsonSerializer.Serialize(_plate));
            }

            return StatusCode(501);
        }

        //[HttpPut]
        //[Route("updateplate")]
        //public ActionResult<Plate> UpdatePlate(Plate plate)
        //{
        //    var _plate = _platesHandler.UpdatePlate(plate);

        //    return Ok(JsonSerializer.Serialize(_plate));
        //}

    }
}
