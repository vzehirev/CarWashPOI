using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarWashPOI.Data;
using CarWashPOI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarWashPOI.Controllers.Api
{
    [Route("Api/[controller]")]
    [ApiController]
    public class GetLocationsController : ControllerBase
    {
        private readonly ILocationsService locationsService;

        public GetLocationsController(ILocationsService locationsService)
        {
            this.locationsService = locationsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var allLocations = await locationsService.GetAllLocationsAsync();

            return new JsonResult(allLocations);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetLocationDetails(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}