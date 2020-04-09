using AutoMapper;
using CarWashPOI.Data.Models;
using CarWashPOI.ViewModels;
using CarWashPOI.ViewModels.Locations;
using CarWashPOI.ViewModels.LocationTypes;
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
                .ForMember(dest => dest.LatLon, opt => opt.MapFrom(src => src));

            CreateMap<AddLocationViewModel, Address>();

            CreateMap<AddLocationViewModel, Coordinates>();

            //CreateMap<AddLocationInputModel, Address>();
            //CreateMap<AddLocationInputModel, WashLocation>()
            //    .ForMember(dst => dst.Address, opt => opt.MapFrom(src => src));
            //CreateMap<Address, AddLocationInputModel>();
            //CreateMap<WashLocation, AddLocationInputModel>()
            //    .ForMember(dst => dst.Town, opt => opt.MapFrom(src => src.Address.Town))
            //    .ForMember(dst => dst.Neighbourhood, opt => opt.MapFrom(src => src.Address.Neighbourhood))
            //    .ForMember(dst => dst.Street, opt => opt.MapFrom(src => src.Address.Street));
        }
    }
}
