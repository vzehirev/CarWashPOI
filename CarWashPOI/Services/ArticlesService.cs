using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarWashPOI.Data;
using CarWashPOI.Data.Models;
using CarWashPOI.ViewModels.Articles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public class ArticlesService : IArticlesService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public ArticlesService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<int> AddArticleAsync(AddArticleViewModel inputModel)
        {
            var articleToAdd = mapper.Map<Article>(inputModel);

            dbContext.Articles.Add(articleToAdd);
            await dbContext.SaveChangesAsync();

            return articleToAdd.Id;
        }

        public async Task<ReadArticleOutputModel> GetArticleByIdAsync(int articleId)
        {
            return await dbContext.Articles
                .Where(a => a.Id == articleId)
                .ProjectTo<ReadArticleOutputModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<ArticlesIndexOutputModel> GetArticlesAsync(int skip, int take, string orderBy)
        {
            var query = dbContext.Articles.AsQueryable();

            if (orderBy == "views")
            {
                query = query.OrderByDescending(a => a.Views);
            }
            else
            {
                query = query.OrderByDescending(a => a.LastModified);
            }

            var outputModel = new ArticlesIndexOutputModel
            {
                AllArticles = await query.CountAsync(),
                Articles = await query
                    .Skip(skip)
                    .Take(take)
                    .ProjectTo<ArticleOutputModel>(mapper.ConfigurationProvider)
                    .ToArrayAsync(),
                OrderedBy = orderBy,
            };

            return outputModel;
        }
    }
}
