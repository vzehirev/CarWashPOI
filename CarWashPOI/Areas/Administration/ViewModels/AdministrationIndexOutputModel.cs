using CarWashPOI.Areas.Administration.ViewModels.Articles;
using CarWashPOI.Areas.Administration.ViewModels.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Areas.Administration.ViewModels
{
    public class AdministrationIndexOutputModel
    {
        public IEnumerable<ArticleForApprovalOutputModel> ArticlesForApproval { get; set; }

        public IEnumerable<LocationForApprovalOutputModel> LocationsForApproval { get; set; }
    }
}
