using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashPOI.Services;
using CarWashPOI.Services.Towns;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarWashPOI.Controllers.Rest
{
    [Route("Rest/Towns")]
    [ApiController]
    public class TownsRestController : ControllerBase
    {
        private readonly ITownsService townsService;

        public TownsRestController(ITownsService townsService)
        {
            this.townsService = townsService;
        }

        [HttpGet("{townId:int}")]
        public async Task<IActionResult> GetTownCoordinates(int townId)
        {
            var townCoordinates = await townsService.GetTownCoordinatesAsync(townId);

            return new JsonResult(townCoordinates);
        }
    }
}