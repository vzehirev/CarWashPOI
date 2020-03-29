using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CarWashPOI.Data.Models
{
    public class ApplicationUser : IdentityUser, IDeleteableEntity
    {
        public ICollection<UserRating> UserRatings { get; set; }
        public bool IsDeleted { get; set; }
    }
}
