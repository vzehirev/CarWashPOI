using CarWashPOI.ViewModels.Coordinates;
using CarWashPOI.ViewModels.Towns;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public interface ITownsService
    {
        public Task<IEnumerable<TownViewModel>> GetAllTownsAsync();

        public Task<CoordinatesOutputModel> GetTownCoordinatesAsync(int townId);
    }
}
