using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.ViewModels.Articles
{
    public class AddArticleViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
