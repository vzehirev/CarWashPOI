using System.Collections.Generic;

namespace CarWashPOI.Data.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Positives { get; set; }
        public int Negatives { get; set; }
        public ICollection<UserRating> RatingUsers { get; set; }
    }
}
