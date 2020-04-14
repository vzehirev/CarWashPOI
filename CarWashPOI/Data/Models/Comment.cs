using System;
using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public class Comment : IDeleteableEntity
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime LastModified { get; set; }

        public bool IsDeleted { get; set; }
    }
}
