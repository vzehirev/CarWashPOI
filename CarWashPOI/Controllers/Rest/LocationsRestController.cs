using CarWashPOI.Data.Models;
using CarWashPOI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashPOI.Controllers.Rest
{
    [Route("/Rest/Locations")]
    [ApiController]
    public class LocationsRestController : ControllerBase
    {
        private readonly ILocationsService locationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public LocationsRestController(ILocationsService locationsService, UserManager<ApplicationUser> userManager)
        {
            this.locationsService = locationsService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            IEnumerable<ViewModels.Locations.LocationRestResponseModel> allLocations = await locationsService.GetAllLocationsAsync();

            return new JsonResult(allLocations);
        }

        [HttpGet("{locationId:int}")]
        public async Task<IActionResult> GetLocationCoordinates(int locationId)
        {
            ViewModels.Coordinates.CoordinatesOutputModel locationCoordinates = await locationsService.GetLocationCoordinatesAsync(locationId);

            return new JsonResult(locationCoordinates);
        }

        [Authorize]
        [HttpPost("{locationId:int}")]
        public async Task<IActionResult> RatePositive(int locationId, [FromForm] bool isPositive)
        {
            string userId = userManager.GetUserId(User);

            await locationsService.RateLocationAsync(locationId, userId, isPositive);

            ViewModels.Ratings.RatingOutputModel newLocationRating = await locationsService.GetLocationRatingAsync(locationId);

            return new JsonResult(newLocationRating);
        }

        [HttpGet("ByType/{locationTypeId:int}")]
        public async Task<IActionResult> GetLocationsByType(int locationTypeId)
        {
            IEnumerable<ViewModels.Locations.LocationRestResponseModel> locations = await locationsService.GetLocationsByTypeAsync(locationTypeId);

            return new JsonResult(locations);
        }
    }
}