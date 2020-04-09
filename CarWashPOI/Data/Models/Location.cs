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
        public int LatLonId { get; set; }

        public virtual Coordinates LatLon { get; set; }

        [Required]
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        [Required]
        public int LocationTypeId { get; set; }

        public virtual LocationType LocationType { get; set; }

        public virtual ICollection<Image> Images { get; set; } = new HashSet<Image>();

        public virtual ICollection<Comment> Comments { get; set; }

        [Required]
        public int RatingId { get; set; }

        public virtual Rating Rating { get; set; }

        [Required]
        public DateTime LastModified { get; set; }

        public bool IsDeleted { get; set; }
    }
}
