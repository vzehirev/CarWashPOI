using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public class Location : IDeleteableEntity
    {
        [Key, Required]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int CoordinatesId { get; set; }
        public virtual Coordinates Coordinates { get; set; }

        [Required]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }

        [Required]
        public int LocationTypeId { get; set; }
        public virtual LocationType LocationType { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();

        [Required]
        public DateTime AddedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
