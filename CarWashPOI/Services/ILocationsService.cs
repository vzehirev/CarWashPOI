using CarWashPOI.ViewModels.Location.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public interface ILocationsService
    {
        public Task<int> Add(AddLocationInputModel inputModel);
    }
}
