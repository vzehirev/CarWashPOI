using CarWashPOI.ViewModels.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public interface IArticlesService
    {
        Task<int> AddArticleAsync(AddArticleViewModel inputModel);

        Task<ReadArticleOutputModel> GetArticleByIdAsync(int articleId);

        Task<ArticlesIndexOutputModel> GetArticlesAsync(int skip, int take, string orderBy);
    }
}
