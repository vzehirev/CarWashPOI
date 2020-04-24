using CarWashPOI.ViewModels.Coordinates;
using CarWashPOI.ViewModels.Towns;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Towns
{
    public interface ITownsService
    {
        public Task<TownViewModel[]> GetAllTownsAsync();

        public Task<CoordinatesOutputModel> GetTownCoordinatesAsync(int townId);
    }
}
