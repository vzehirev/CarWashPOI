using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public class Town
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? CoordinatesId { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}
