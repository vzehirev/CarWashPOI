using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.ViewModels.Articles
{
    public class ArticleOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime AddedOn { get; set; }

        public int Views { get; set; }
    }
}
