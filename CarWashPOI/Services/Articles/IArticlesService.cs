using CarWashPOI.Areas.Administration.ViewModels.Articles;
using CarWashPOI.ViewModels.Articles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Articles
{
    public interface IArticlesService
    {
        Task<int> AddArticleAsync(AddArticleViewModel inputModel);

        Task<ReadArticleOutputModel> GetArticleByIdAsync(int articleId);

        Task<ArticlesIndexOutputModel> GetArticlesAsync(int skip, int take, string orderBy);

        Task<IEnumerable<ArticleForApprovalOutputModel>> GetNonApprovedArticlesAsync();

        Task<int> ApproveArticleAsync(int id);

        Task<int> DeleteArticleAsync(int id);

        Task<int> IncreaseArticleViewsAsync(int id);
    }
}
