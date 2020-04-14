using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public class Article : IDeleteableEntity
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Image> Images { get; set; } = new HashSet<Image>();

        [Required]
        public DateTime LastModified { get; set; }

        [Required]
        public int Views { get; set; }

        public bool IsDeleted { get; set; }
    }
}
