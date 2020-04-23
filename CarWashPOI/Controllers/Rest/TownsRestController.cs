using CarWashPOI.Services.Towns;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            ViewModels.Coordinates.CoordinatesOutputModel townCoordinates = await townsService.GetTownCoordinatesAsync(townId);

            return new JsonResult(townCoordinates);
        }
    }
}