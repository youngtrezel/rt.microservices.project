﻿using EventBus.Events;
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

        public MarketingController(IPlatesHandler platesHandler, IPublishEndpoint publishEndpoint)
        {
            _platesHandler = platesHandler;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPut]
        [Route("sellplate")]
        public async Task<ActionResult<Plate>> AddPlate(string registration)
        {
            var _plate = await _platesHandler.SellPlate(registration);
            if (_plate != null)
            {
                await _publishEndpoint.Publish(new PlateSoldEvent
                {
                    Id = _plate.Id,
                });

                return Ok(JsonSerializer.Serialize(_plate));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getplates")]
        public ActionResult<IEnumerable<Plate>> GetPlates(int pageNumber, int pageSize)
        {
            var results = _platesHandler.GetPaginationPlates(pageNumber, pageSize);

            return Ok(JsonSerializer.Serialize(results));
        }

        [HttpGet]
        [Route("getfilteredplates")]
        public ActionResult<IEnumerable<Plate>> GetFilteredPlates(string letters, int pageNumber, int pageSize)
        {
            var results = _platesHandler.GetFilteredPlates(letters, pageNumber, pageSize);

            return Ok(JsonSerializer.Serialize(results));
        }
    }
}
