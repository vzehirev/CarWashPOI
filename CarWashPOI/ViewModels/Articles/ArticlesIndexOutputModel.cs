using System.Collections.Generic;

namespace CarWashPOI.ViewModels.Articles
{
    public class ArticlesIndexOutputModel
    {
        public IEnumerable<ArticleOutputModel> Articles { get; set; }

        public string OrderedBy { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public int AllArticles { get; set; }
    }
}
