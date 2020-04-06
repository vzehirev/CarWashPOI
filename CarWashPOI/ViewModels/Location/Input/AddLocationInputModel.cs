using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.ViewModels.Location.Input
{
    public class AddLocationInputModel
    {
        [Required, Display(Name = "Име")]
        public string Title { get; set; }

        [Required, Display(Name = "Описание")]
        public string Description { get; set; }

        [Required, Display(Name = "Град")]
        public string City { get; set; }

        [Required, Display(Name = "Район")]
        public string Neighbourhood { get; set; }

        [Required, Display(Name = "Адрес")]
        public string Street { get; set; }
    }
}
