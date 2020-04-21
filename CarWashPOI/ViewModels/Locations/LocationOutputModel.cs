using CarWashPOI.ViewModels.Addresses;
using CarWashPOI.ViewModels.Ratings;
using System;

namespace CarWashPOI.ViewModels.Locations
{
    public class LocationOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public HomePageAddressOutputModel Address { get; set; }

        public string ImageUrl { get; set; }

        public RatingOutputModel Rating { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
