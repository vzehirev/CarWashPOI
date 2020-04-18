using CarWashPOI.ViewModels.Addresses;
using CarWashPOI.ViewModels.Comments;
using CarWashPOI.ViewModels.Ratings;
using System;
using System.Collections.Generic;

namespace CarWashPOI.ViewModels.Locations
{
    public class LocationDetailsOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public LocationDetailsPageAddressOutputModel Address { get; set; }

        public RatingOutputModel Rating { get; set; }

        public DateTime LastModified { get; set; }

        public IEnumerable<CommentOutputModel> Comments { get; set; }
    }
}
