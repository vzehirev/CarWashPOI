using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarWashPOI.Areas.Administration.ViewModels.Articles;
using CarWashPOI.Data;
using CarWashPOI.Data.Models;
using CarWashPOI.Services.Images;
using CarWashPOI.ViewModels.Articles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Articles
{
    public class ArticlesService : IArticlesService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IImagesService imagesService;

        public ArticlesService(ApplicationDbContext dbContext, IMapper mapper, IImagesService imagesService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.imagesService = imagesService;
        }

        public async Task<int> AddArticleAsync(AddArticleViewModel inputModel)
        {
            var articleToAdd = mapper.Map<Article>(inputModel);

            const int maxImageSize = 1024 * 1024 * 10;

            if (inputModel.Image != null)
            {
                if (inputModel.Image.ContentType.ToLower().Contains("image") && inputModel.Image.Length <= maxImageSize)
                {
                    using (System.IO.Stream imageFileStream = inputModel.Image.OpenReadStream())
                    {
                        string imageUrl = await imagesService.UploadImageAsync(imageFileStream);
                        articleToAdd.Image = new Image { Url = imageUrl };
                    }
                }
            }

            articleToAdd.AddedOn = DateTime.UtcNow;
            dbContext.Articles.Add(articleToAdd);
            await dbContext.SaveChangesAsync();

            return articleToAdd.Id;
        }

        public async Task<int> ApproveArticleAsync(int id)
        {
            var article = await dbContext.Articles
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (article != null)
            {
                article.IsApproved = true;
            }

            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteArticleAsync(int id)
        {
            var article = await dbContext.Articles
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (article != null)
            {
                article.IsDeleted = true;
            }

            return await dbContext.SaveChangesAsync();
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
            var query = dbContext.Articles.Where(a => a.IsApproved && !a.IsDeleted).AsQueryable();

            if (orderBy == "views")
            {
                query = query.OrderByDescending(a => a.Views);
            }
            else
            {
                query = query.OrderByDescending(a => a.AddedOn);
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

        public async Task<IEnumerable<ArticleForApprovalOutputModel>> GetNonApprovedArticlesAsync()
        {
            return await dbContext.Articles
                .Where(a => !a.IsApproved && !a.IsDeleted)
                .ProjectTo<ArticleForApprovalOutputModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task<int> IncreaseArticleViewsAsync(int id)
        {
            var article = await dbContext.Articles
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (article != null)
            {
                article.Views += 1;
            }

            return await dbContext.SaveChangesAsync();
        }
    }
}
