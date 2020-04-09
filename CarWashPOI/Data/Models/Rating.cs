using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public class Rating
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public int Positives { get; set; }

        [Required]
        public int Negatives { get; set; }

        public virtual ICollection<UserRating> RatingUsers { get; set; }
    }
}
