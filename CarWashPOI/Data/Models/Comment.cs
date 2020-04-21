using System;
using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public class Comment
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime AddedOn { get; set; }
    }
}
