using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public class Address
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        public string Neighbourhood { get; set; }

        public string Street { get; set; }
    }
}
