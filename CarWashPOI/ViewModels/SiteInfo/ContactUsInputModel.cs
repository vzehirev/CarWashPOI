using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.ViewModels.SiteInfo
{
    public class ContactUsInputModel
    {
        [Required(ErrorMessage = "Полето E-mail е задължителтно!")]
        [Display(Name = "E-mail*")]
        [EmailAddress(ErrorMessage = "Невалиден e-mail!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето Съобщение е задължително!"), Display(Name = "Съобщение*")]
        public string Message { get; set; }
    }
}
