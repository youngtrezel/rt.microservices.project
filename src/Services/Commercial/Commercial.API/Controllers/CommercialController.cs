using Commercial.API.Interfaces;
using Commercial.Domain.Models;
using Commercial.Domain.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Commercial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommercialController : ControllerBase
    {

        private readonly IPlatesHandler _platesHandler;

        public CommercialController(IPlatesHandler platesHandler)
        {
            _platesHandler = platesHandler;
        }

        [Route("getplate")]
        public ActionResult<IEnumerable<Plate>> GetPlate(string registration)
        {
            var result = _platesHandler.GetPlate(registration);

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
        public ActionResult AddPlate(PlateDto plate)
        {
            _platesHandler.AddPlate(plate);

            return Ok(JsonSerializer.Serialize(plate));
        }

        [HttpPut]
        [Route("updateplate")]
        public ActionResult UpdatePlate(Plate plate)
        {
            var _plate = _platesHandler.UpdatePlate(plate);

            return Ok(JsonSerializer.Serialize(_plate));
        }

        [HttpPut]
        [Route("reserveplate")]
        public ActionResult ReservePlate(Plate plate)
        {
            var _plate = _platesHandler.ReservePlate(plate);

            return Ok(JsonSerializer.Serialize(_plate));
        }
    }
}
