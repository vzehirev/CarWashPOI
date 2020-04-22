using CarWashPOI.Areas.Administration.ViewModels.Locations;
using CarWashPOI.ViewModels;
using CarWashPOI.ViewModels.Coordinates;
using CarWashPOI.ViewModels.Locations;
using CarWashPOI.ViewModels.Ratings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Locations
{
    public interface ILocationsService
    {
        Task<int> AddAsync(AddLocationViewModel inputModel);

        Task<IEnumerable<LocationRestResponseModel>> GetAllLocationsAsync();

        Task<LocationDetailsOutputModel> GetLocationDetailsAsync(int locationId);

        Task<CoordinatesOutputModel> GetLocationCoordinatesAsync(int locationId);

        Task<int> RateLocationAsync(int locationId, string userId, bool isPositive);

        Task<RatingOutputModel> GetLocationRatingAsync(int locationId);

        Task<HomePageOutputModel> GetLocationsAsync(int townId, int typeId, string orderBy, int skip, int take);

        Task<int> ApproveLocationAsync(int id);

        Task<IEnumerable<LocationRestResponseModel>> GetLocationsByTypeAsync(int locationTypeId);

        Task<IEnumerable<LocationForApprovalOutputModel>> GetNonApprovedLocationsAsync();

        Task<int> DeleteLocationAsync(int id);
    }
}
