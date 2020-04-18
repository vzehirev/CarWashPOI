using CarWashPOI.ViewModels.Addresses;

namespace CarWashPOI.ViewModels.Locations
{
    public class LocationOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public HomePageAddressOutputModel Address { get; set; }

        public string ImageUrl { get; set; }
    }
}
