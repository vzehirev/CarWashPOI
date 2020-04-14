using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CarWashPOI.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
    }
}
