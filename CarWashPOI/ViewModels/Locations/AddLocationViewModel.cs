using CarWashPOI.ViewModels.LocationTypes;
using CarWashPOI.ViewModels.Towns;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarWashPOI.ViewModels.Locations
{
    public class AddLocationViewModel
    {

        [Required(ErrorMessage = "Полето Тип е задължително."), Display(Name = "Тип*")]
        public int LocationTypeId { get; set; }

        [Display(Name = "Име")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Полето Град е задължително."), Display(Name = "Град*")]
        public int TownId { get; set; }

        [Display(Name = "Район")]
        public string Neighbourhood { get; set; }

        [Display(Name = "Адрес")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Локацията не е отбелязана с маркер на картата.")]
        public double? Latitude { get; set; }

        [Required]
        public double? Longitude { get; set; }

        [Display(Name = "Снимка")]
        public IFormFile Image { get; set; }

        public IEnumerable<LocationTypeViewModel> AllLocationTypes { get; set; }

        public IEnumerable<TownViewModel> AllTowns { get; set; }
    }
}
