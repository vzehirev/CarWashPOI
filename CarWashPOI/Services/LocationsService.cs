using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarWashPOI.Areas.Administration.ViewModels.Locations;
using CarWashPOI.Data;
using CarWashPOI.Data.Models;
using CarWashPOI.ViewModels;
using CarWashPOI.ViewModels.Coordinates;
using CarWashPOI.ViewModels.Locations;
using CarWashPOI.ViewModels.Ratings;
using Microsoft.EntityFrameworkCore;
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
            Location locationToAdd = mapper.Map<Location>(addLocationViewModel);

            const int maxImageSize = 1024 * 1024 * 10;
            if (addLocationViewModel.Image != null)
            {
                if (addLocationViewModel.Image.ContentType.ToLower().Contains("image") && addLocationViewModel.Image.Length <= maxImageSize)
                {
                    using (System.IO.Stream imageFileStream = addLocationViewModel.Image.OpenReadStream())
                    {
                        string imageUrl = await imagesService.UploadImageAsync(imageFileStream);
                        locationToAdd.Image = new Image { Url = imageUrl };
                    }
                }
            }

            dbContext.Locations.Add(locationToAdd);
            await dbContext.SaveChangesAsync();

            return locationToAdd.Id;
        }

        public async Task<int> ApproveLocationAsync(int id)
        {
            var location = await dbContext.Locations
                .Where(l => l.Id == id)
                .FirstOrDefaultAsync();

            if (location!=null)
            {
                location.IsApproved = true;
            }

            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteLocationAsync(int id)
        {
            var location = await dbContext.Locations
                .Where(l => l.Id == id)
                .FirstOrDefaultAsync();

            if (location != null)
            {
                location.IsDeleted = true;
            }

            return await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<LocationRestResponseModel>> GetAllLocationsAsync()
        {
            LocationRestResponseModel[] allLocations = await dbContext.Locations
                .Where(l=>l.IsApproved && !l.IsDeleted)
                .ProjectTo<LocationRestResponseModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return allLocations;
        }

        public async Task<CoordinatesOutputModel> GetLocationCoordinatesAsync(int id)
        {
            CoordinatesOutputModel locationCoordinates = await dbContext.Locations
                .Where(l => l.Id == id)
                .ProjectTo<CoordinatesOutputModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return locationCoordinates;
        }

        public async Task<LocationDetailsOutputModel> GetLocationDetailsAsync(int id)
        {
            LocationDetailsOutputModel location = await dbContext.Locations
                .Where(l => l.Id == id)
                .ProjectTo<LocationDetailsOutputModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return location;
        }

        public async Task<RatingOutputModel> GetLocationRatingAsync(int locationId)
        {
            return await dbContext.Locations
                .Where(l => l.Id == locationId)
                .ProjectTo<RatingOutputModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<HomePageOutputModel> GetLocationsAsync(int townId, int typeId, string orderBy, int skip, int take)
        {
            HomePageOutputModel outputModel = new HomePageOutputModel();

            IQueryable<Location> query = dbContext.Locations.Where(l => l.IsApproved && !l.IsDeleted).AsQueryable();

            if (townId != 0)
            {
                query = query.Where(l => l.Address.TownId == townId);
            }

            if (typeId != 0)
            {
                query = query.Where(l => l.LocationTypeId == typeId);
            }

            if (orderBy == "rating")
            {
                query = query
                    .OrderByDescending(l => l.Ratings.
                    Where(r => r.IsPositive)
                        .Count()
                    - l.Ratings.
                    Where(r => !r.IsPositive)
                        .Count());
            }
            else
            {
                query = query.OrderByDescending(l => l.AddedOn);
            }

            outputModel.AllLocations = await query.CountAsync();

            outputModel.Locations = await query
                .Skip(skip)
                .Take(take)
                .ProjectTo<LocationOutputModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return outputModel;
        }

        public async Task<IEnumerable<LocationRestResponseModel>> GetLocationsByTypeAsync(int locationTypeId)
        {
            LocationRestResponseModel[] locations = await dbContext.Locations
                .Where(l => l.IsApproved && !l.IsDeleted && l.LocationTypeId == locationTypeId)
                .ProjectTo<LocationRestResponseModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();

            return locations;
        }

        public async Task<IEnumerable<LocationForApprovalOutputModel>> GetNonApprovedLocationsAsync()
        {
            return await dbContext.Locations
                .Where(l => !l.IsApproved && !l.IsDeleted)
                .ProjectTo<LocationForApprovalOutputModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task<int> RateLocationAsync(int locationId, string userId, bool isPositive)
        {
            ApplicationUser user = await dbContext.Users.Include(u => u.Ratings).FirstOrDefaultAsync(u => u.Id == userId);
            Rating usersRating = user.Ratings.FirstOrDefault(r => r.LocationId == locationId);

            if (usersRating != null)
            {
                usersRating.IsPositive = isPositive;
            }
            else
            {
                usersRating = new Rating
                {
                    LocationId = locationId,
                    UserId = userId,
                    IsPositive = isPositive,
                };

                dbContext.Ratings.Add(usersRating);
            }

            return await dbContext.SaveChangesAsync();
        }
    }
}
