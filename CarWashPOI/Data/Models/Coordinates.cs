using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public class Coordinates
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
