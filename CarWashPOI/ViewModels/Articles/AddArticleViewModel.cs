using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.ViewModels.Articles
{
    public class AddArticleViewModel
    {
        [Required(ErrorMessage = "Полето Заглавие е задължително.")]
        [MinLength(4, ErrorMessage = "Полето Заглавие трябва да е с дължина от минимум 4 символа")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Статията трябва да има съдържание.")]
        [MinLength(11, ErrorMessage = "Полето Съдържание трябва да е с дължина от минимум 11 символа")]
        public string Content { get; set; }

        public string UserId { get; set; }

        public IFormFile Image { get; set; }
    }
}
