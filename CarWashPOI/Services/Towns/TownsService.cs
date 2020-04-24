using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarWashPOI.Data;
using CarWashPOI.ViewModels.Coordinates;
using CarWashPOI.ViewModels.Towns;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Towns
{
    public class TownsService : ITownsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public TownsService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<TownViewModel[]> GetAllTownsAsync()
        {
            TownViewModel[] allTowns = await dbContext.Towns
                .ProjectTo<TownViewModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return allTowns;
        }

        public async Task<CoordinatesOutputModel> GetTownCoordinatesAsync(int townId)
        {
            return await dbContext.Towns
                .Where(t => t.Id == townId)
                .ProjectTo<CoordinatesOutputModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
    }
}
