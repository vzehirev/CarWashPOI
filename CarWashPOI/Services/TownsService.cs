using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarWashPOI.Data;
using CarWashPOI.ViewModels.Towns;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashPOI.Services
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

        public async Task<IEnumerable<TownViewModel>> GetAllTownsAsync()
        {
            TownViewModel[] allTowns = await dbContext.Towns
                .ProjectTo<TownViewModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return allTowns;
        }
    }
}
