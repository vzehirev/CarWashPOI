using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.ViewModels.Articles
{
    public class AddArticleViewModel
    {
        [Required(ErrorMessage = "Полето Заглавие е задължително.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Статията трябва да има съдържание.")]
        public string Content { get; set; }

        public string UserId { get; set; }

        public IFormFile Image { get; set; }
    }
}
