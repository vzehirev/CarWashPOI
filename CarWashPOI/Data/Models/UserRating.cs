using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public class UserRating : IDeleteableEntity
    {
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int RatingId { get; set; }

        public virtual Rating Rating { get; set; }

        public bool IsDeleted { get; set; }
    }
}
