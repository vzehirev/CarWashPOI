using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarWashPOI.Areas.Administration.ViewModels.Articles;
using CarWashPOI.Data;
using CarWashPOI.Data.Models;
using CarWashPOI.Services.Images;
using CarWashPOI.ViewModels.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;
        private readonly IImagesService imagesService;
        private readonly long maxImageSize;

        public ArticlesService(ApplicationDbContext dbContext,
            IMapper mapper,
            IConfiguration configuration,
            IImagesService imagesService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.configuration = configuration;
            this.imagesService = imagesService;
            maxImageSize = long.Parse(this.configuration["MaxImageSize"]);
        }

        public async Task<int> AddArticleAsync(AddArticleViewModel inputModel)
        {
            Article articleToAdd = mapper.Map<Article>(inputModel);

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
            Article article = await dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (article != null)
            {
                article.IsApproved = true;
            }

            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteArticleAsync(int id)
        {
            Article article = await dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (article != null)
            {
                article.IsDeleted = true;
            }

            return await dbContext.SaveChangesAsync();
        }

        public async Task<ReadArticleOutputModel> GetArticleByIdAsUserAsync(int articleId)
        {
            return await dbContext.Articles
                .Where(a => a.Id == articleId && a.IsApproved && !a.IsDeleted)
                .ProjectTo<ReadArticleOutputModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<ReadArticleOutputModel> GetArticleByIdAsAdminAsync(int articleId)
        {
            return await dbContext.Articles
                .Where(a => a.Id == articleId)
                .ProjectTo<ReadArticleOutputModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<ArticlesIndexOutputModel> GetArticlesAsync(int skip, int take, string orderBy)
        {
            IQueryable<Article> query = dbContext.Articles.Where(a => a.IsApproved && !a.IsDeleted).AsQueryable();

            if (orderBy == "views")
            {
                query = query.OrderByDescending(a => a.Views);
            }
            else
            {
                query = query.OrderByDescending(a => a.AddedOn);
            }

            ArticlesIndexOutputModel outputModel = new ArticlesIndexOutputModel
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

        public async Task<int> GetTotalArticlesAsync()
        {
            return await dbContext.Articles
                .CountAsync(a => a.IsApproved && !a.IsDeleted);
        }

        public async Task<int> IncreaseArticleViewsAsync(int id)
        {
            Article article = await dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (article != null)
            {
                article.Views++;
            }

            return await dbContext.SaveChangesAsync();
        }
    }
}
