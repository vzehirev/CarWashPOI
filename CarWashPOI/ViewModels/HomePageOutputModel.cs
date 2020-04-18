using CarWashPOI.ViewModels.Locations;
using CarWashPOI.ViewModels.LocationTypes;
using CarWashPOI.ViewModels.Towns;
using System.Collections.Generic;

namespace CarWashPOI.ViewModels
{
    public class HomePageOutputModel
    {
        public IEnumerable<LocationOutputModel> Locations { get; set; }

        public IEnumerable<TownViewModel> AllTowns { get; set; }

        public IEnumerable<LocationTypeViewModel> AllTypes { get; set; }

        public int SelectedTownId { get; set; }

        public int SelectedTypeId { get; set; }

        public string SelectedOrderBy { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public int AllCases { get; set; }
    }
}
