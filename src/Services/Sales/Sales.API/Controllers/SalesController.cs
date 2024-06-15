using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.API.Interfaces;
using Sales.Domain.Models;
using System.Text.Json;

namespace Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IPlatesHandler _platesHandler;
        public SalesController(IPlatesHandler platesHandler)
        {
            _platesHandler = platesHandler;
        }

        [HttpGet]
        [Route("getplates")]
        public ActionResult<IEnumerable<Plate>> GetPlates(int pageNumber, int pageSize)
        {
            var results = _platesHandler.GetUnreservedPlates(pageNumber, pageSize);

            return Ok(JsonSerializer.Serialize(results));
        }
    }
}
