using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public class Article : IDeleteableEntity, IApproveableEntity
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

        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }

        [Required]
        public DateTime AddedOn { get; set; }

        [Required]
        public int Views { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsApproved { get; set; }
    }
}
