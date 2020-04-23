using CarWashPOI.Areas.Administration.ViewModels.Articles;
using CarWashPOI.Areas.Administration.ViewModels.Locations;
using System.Collections.Generic;

namespace CarWashPOI.Areas.Administration.ViewModels
{
    public class AdministrationIndexOutputModel
    {
        public IEnumerable<ArticleForApprovalOutputModel> ArticlesForApproval { get; set; }

        public IEnumerable<LocationForApprovalOutputModel> LocationsForApproval { get; set; }

        public int TotalLocations { get; set; }

        public int TotalArticles { get; set; }

        public int TotalUsers { get; set; }
    }
}
