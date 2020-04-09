using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarWashPOI.Data;
using CarWashPOI.ViewModels.LocationTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public class LocationTypesService : ILocationTypesService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper autoMapper;

        public LocationTypesService(ApplicationDbContext dbContext, IMapper autoMapper)
        {
            this.dbContext = dbContext;
            this.autoMapper = autoMapper;
        }
        public async Task<IEnumerable<LocationTypeViewModel>> GetAllLocationTypesAsync()
        {
            var allLocationTypes = await dbContext.LocationTypes
                .ProjectTo<LocationTypeViewModel>(autoMapper.ConfigurationProvider)
                .ToArrayAsync();

            return allLocationTypes;
        }
    }
}
