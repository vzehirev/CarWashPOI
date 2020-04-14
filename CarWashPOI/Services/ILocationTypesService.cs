using CarWashPOI.ViewModels.LocationTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public interface ILocationTypesService
    {
        public Task<IEnumerable<LocationTypeViewModel>> GetAllLocationTypesAsync();
    }
}
