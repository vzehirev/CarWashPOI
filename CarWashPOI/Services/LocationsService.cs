using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarWashPOI.Data;
using CarWashPOI.Data.Models;
using CarWashPOI.ViewModels.Locations;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IImagesService imagesService;

        public LocationsService(ApplicationDbContext dbContext, IMapper mapper, IImagesService imagesService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.imagesService = imagesService;
        }

        public async Task<int> AddAsync(AddLocationViewModel addLocationViewModel)
        {
            var locationToAdd = mapper.Map<Location>(addLocationViewModel);

            const int maxImageSize = 1024 * 1024 * 10;
            if (addLocationViewModel.Files != null)
            {
                foreach (var image in addLocationViewModel.Files)
                {
                    if (image.ContentType.ToUpper().Contains("IMAGE") && image.Length <= maxImageSize)
                    {
                        using (var imageFileStream = image.OpenReadStream())
                        {
                            var imageUrl = await this.imagesService.UploadImageAsync(imageFileStream);
                            locationToAdd.Images.Add(new Image { Url = imageUrl });
                        }
                    }
                }
            }

            locationToAdd.Rating = new Rating();

            dbContext.Locations.Add(locationToAdd);
            await dbContext.SaveChangesAsync();

            return locationToAdd.Id;
        }

        public async Task<IEnumerable<LocationOutputModel>> GetAllLocationsAsync()
        {
            var allLocations = await dbContext.Locations
                .ProjectTo<LocationOutputModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return allLocations;
        }

        public async Task<LocationDetailsOutputModel> GetLocationDetailsAsync(int id)
        {
            var location = await dbContext.Locations
                .Where(l => l.Id == id)
                .ProjectTo<LocationDetailsOutputModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return location;
        }
    }
}
