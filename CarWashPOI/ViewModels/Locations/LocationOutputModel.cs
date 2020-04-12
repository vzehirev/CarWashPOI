using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.ViewModels.Locations
{
    public class LocationOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
