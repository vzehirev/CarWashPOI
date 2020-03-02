using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Data.Models
{
    public class Article : IDeleteableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Image> Images { get; set; }
        public DateTime LastModified { get; set; }
        public int Views { get; set; }
        public bool IsDeleted { get; set; }
    }
}
