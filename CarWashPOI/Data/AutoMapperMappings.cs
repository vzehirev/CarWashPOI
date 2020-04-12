using AutoMapper;
using CarWashPOI.Data.Models;
using CarWashPOI.ViewModels;
using CarWashPOI.ViewModels.Addresses;
using CarWashPOI.ViewModels.Comments;
using CarWashPOI.ViewModels.Locations;
using CarWashPOI.ViewModels.LocationTypes;
using CarWashPOI.ViewModels.Ratings;
using CarWashPOI.ViewModels.Towns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.LocationType.Type));

            CreateMap<Address, AddressOutputModel>()
                .ForMember(dest => dest.Town, opt => opt.MapFrom(src => src.Town.Name));

            CreateMap<Comment, CommentOutputModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<Rating, RatingOutputModel>();
        }
    }
}
