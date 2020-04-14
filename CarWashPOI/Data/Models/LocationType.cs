using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public class LocationType
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
