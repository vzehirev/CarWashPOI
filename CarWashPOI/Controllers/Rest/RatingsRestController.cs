using CarWashPOI.Data.Models;
using CarWashPOI.Services.Locations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarWashPOI.Controllers.Rest
{
    [Authorize]
    [Route("Rest/Rate")]
    [ApiController]
    public class RatingsRestController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILocationsService locationsService;

        public RatingsRestController(UserManager<ApplicationUser> userManager, ILocationsService locationsService)
        {
            this.userManager = userManager;
            this.locationsService = locationsService;
        }

        [HttpPost("{locationId:int}")]
        public async Task<IActionResult> RateLocation(int locationId, [FromForm] bool isPositive)
        {
            string userId = userManager.GetUserId(User);

            await locationsService.RateLocationAsync(locationId, userId, isPositive);

            ViewModels.Ratings.RatingOutputModel newLocationRating = await locationsService.GetLocationRatingAsync(locationId);

            return new JsonResult(newLocationRating);
        }
    }
}