using AutoMapper;
using CarWashPOI.Data.Models;
using CarWashPOI.ViewModels.Addresses;
using CarWashPOI.ViewModels.Comments;
using CarWashPOI.ViewModels.Coordinates;
using CarWashPOI.ViewModels.Locations;
using CarWashPOI.ViewModels.LocationTypes;
using CarWashPOI.ViewModels.Ratings;
using CarWashPOI.ViewModels.Towns;
using System.Linq;

namespace CarWashPOI.Data
{
    public class AutoMapperMappings : Profile
    {
        public AutoMapperMappings()
        {
            CreateMap<Town, TownViewModel>();

            CreateMap<LocationType, LocationTypeViewModel>();

            CreateMap<AddLocationViewModel, Location>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Coordinates, opt => opt.MapFrom(src => src));

            CreateMap<AddLocationViewModel, Address>();

            CreateMap<AddLocationViewModel, Coordinates>();

            CreateMap<Location, LocationOutputModel>()
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Coordinates.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Coordinates.Longitude));

            CreateMap<Location, LocationDetailsOutputModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.LocationType.Type))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src));

            CreateMap<Location, RatingOutputModel>()
                .ForMember(dest => dest.Positives, opt => opt.MapFrom(src => src.Ratings.Count(r => r.IsPositive)))
                .ForMember(dest => dest.Negatives, opt => opt.MapFrom(src => src.Ratings.Count(r => !r.IsPositive)));

            CreateMap<Address, AddressOutputModel>()
                .ForMember(dest => dest.Town, opt => opt.MapFrom(src => src.Town.Name));

            CreateMap<Comment, CommentOutputModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<Location, CoordinatesOutputModel>()
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Coordinates.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Coordinates.Longitude));
        }
    }
}
