using CarWashPOI.Services.Articles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarWashPOI.Controllers.Rest
{
    [Route("Rest/Articles")]
    [ApiController]
    public class ArticlesRestController : ControllerBase
    {
        private readonly IArticlesService articlesService;

        public ArticlesRestController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        [HttpPost("{id:int}")]
        public async Task<int> IncreaseArticleViews(int id)
        {
            return await articlesService.IncreaseArticleViewsAsync(id);
        }
    }
}