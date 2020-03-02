using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Data.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Town { get; set; }
        public string Neighbourhood { get; set; }
        public string Street { get; set; }
    }
}
