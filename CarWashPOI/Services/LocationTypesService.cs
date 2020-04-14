using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarWashPOI.Data;
using CarWashPOI.ViewModels.LocationTypes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public class LocationTypesService : ILocationTypesService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public LocationTypesService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<LocationTypeViewModel>> GetAllLocationTypesAsync()
        {
            LocationTypeViewModel[] allLocationTypes = await dbContext.LocationTypes
                .ProjectTo<LocationTypeViewModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return allLocationTypes;
        }
    }
}
