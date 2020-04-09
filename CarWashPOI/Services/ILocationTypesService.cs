using CarWashPOI.ViewModels.LocationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public interface ILocationTypesService
    {
        public Task<IEnumerable<LocationTypeViewModel>> GetAllLocationTypesAsync();
    }
}
