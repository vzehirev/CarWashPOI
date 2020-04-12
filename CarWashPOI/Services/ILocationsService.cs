using CarWashPOI.ViewModels.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public interface ILocationsService
    {
        public Task<int> AddAsync(AddLocationViewModel inputModel);

        public Task<IEnumerable<LocationOutputModel>> GetAllLocationsAsync();

        public Task<LocationDetailsOutputModel> GetLocationDetailsAsync(int id);
    }
}
