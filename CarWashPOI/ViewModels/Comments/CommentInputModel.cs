using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.ViewModels.Comments
{
    public class CommentInputModel
    {
        [Required]
        public int LocationId { get; set; }

        [Required]
        [MinLength(3)]
        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
