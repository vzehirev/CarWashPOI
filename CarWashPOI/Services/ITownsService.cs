using CarWashPOI.ViewModels.Towns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public interface ITownsService
    {
        public Task<IEnumerable<TownViewModel>> GetAllTownsAsync();
    }
}
