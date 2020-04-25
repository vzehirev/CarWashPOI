using CarWashPOI.ViewModels.LocationTypes;
using System.Threading.Tasks;

namespace CarWashPOI.Services.LocationTypes
{
    public interface ILocationTypesService
    {
        public Task<LocationTypeViewModel[]> GetAllLocationTypesAsync();
    }
}
