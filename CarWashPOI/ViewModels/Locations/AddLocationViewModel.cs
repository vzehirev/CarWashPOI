using CarWashPOI.ViewModels.LocationTypes;
using CarWashPOI.ViewModels.Towns;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.ViewModels.Locations
{
    public class AddLocationViewModel
    {

        [Required, Display(Name = "Тип*")]
        public int LocationTypeId { get; set; }

        [Display(Name = "Име")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required, Display(Name = "Град*")]
        public int TownId { get; set; }

        [Display(Name = "Район")]
        public string Neighbourhood { get; set; }

        [Display(Name = "Адрес")]
        public string Street { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [Display(Name = "Снимки")]
        public List<IFormFile> Files { get; set; }

        public IEnumerable<LocationTypeViewModel> AllLocationTypes { get; set; }

        public IEnumerable<TownViewModel> AllTowns { get; set; }
    }
}
