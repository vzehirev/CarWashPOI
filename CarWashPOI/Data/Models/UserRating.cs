﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Data.Models
{
    public class UserRating : IDeleteableEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
        public bool IsDeleted { get; set; }
    }
}
