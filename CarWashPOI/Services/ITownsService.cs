using CarWashPOI.ViewModels.Towns;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public interface ITownsService
    {
        public Task<IEnumerable<TownViewModel>> GetAllTownsAsync();
    }
}
