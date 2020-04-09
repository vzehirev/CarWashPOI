﻿using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.Data.Models
{
    public class Image : IDeleteableEntity
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public bool IsDeleted { get; set; }
    }
}
