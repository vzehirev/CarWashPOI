using System;

namespace CarWashPOI.ViewModels.Articles
{
    public class ArticleOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime AddedOn { get; set; }

        public int Views { get; set; }

        public string ImageUrl { get; set; }
    }
}
