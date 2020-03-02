using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Data.Models
{
    public class ApplicationUser : IdentityUser, IDeleteableEntity
    {
        public ICollection<UserRating> UserRatings { get; set; }
        public bool IsDeleted { get; set; }
    }
}
