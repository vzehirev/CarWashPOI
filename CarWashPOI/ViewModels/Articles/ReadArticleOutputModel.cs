using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.ViewModels.Articles
{
    public class ReadArticleOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime AddedOn { get; set; }

        public string User { get; set; }

        public int Views { get; set; }
    }
}
