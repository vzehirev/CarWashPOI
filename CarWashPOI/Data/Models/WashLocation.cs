using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Data.Models
{
    public class WashLocation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Description { get; set; }
        public LatLon LatLon { get; set; }
        public Address Address { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Rating Rating { get; set; }
        public DateTime LastModified { get; set; }
    }
}
