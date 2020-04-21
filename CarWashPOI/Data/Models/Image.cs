using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public class Image
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
