using CarWashPOI.ViewModels.Addresses;
using CarWashPOI.ViewModels.Comments;
using CarWashPOI.ViewModels.Locations;
using CarWashPOI.ViewModels.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.ViewModels.Locations
{
    public class LocationDetailsOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public AddressOutputModel Address { get; set; }

        public RatingOutputModel Rating { get; set; }

        public DateTime LastModified { get; set; }

        public IEnumerable<CommentOutputModel> Comments { get; set; }
    }
}
