using CarWashPOI.ViewModels.Coordinates;
using CarWashPOI.ViewModels.Locations;
using CarWashPOI.ViewModels.Ratings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public interface ILocationsService
    {
        Task<int> AddAsync(AddLocationViewModel inputModel);

        Task<IEnumerable<LocationOutputModel>> GetAllLocationsAsync();

        Task<LocationDetailsOutputModel> GetLocationDetailsAsync(int locationId);

        Task<CoordinatesOutputModel> GetLocationCoordinatesAsync(int locationId);

        Task<int> RateLocationAsync(int locationId, string userId, bool isPositive);

        Task<RatingOutputModel> GetLocationRatingAsync(int locationId);
    }
}
