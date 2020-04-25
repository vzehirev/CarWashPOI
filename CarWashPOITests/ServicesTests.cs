using AutoMapper;
using CarWashPOI.Data;
using CarWashPOI.Data.Models;
using CarWashPOI.Services.Articles;
using CarWashPOI.Services.Images;
using CarWashPOI.Services.Locations;
using CarWashPOI.ViewModels.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CarWashPOITests
{
    public class ServicesTests
    {
        private readonly DependenciesResolver serviceProvider;

        public ServicesTests()
        {
            serviceProvider = new DependenciesResolver();
        }

        [Fact]
        public void AddLocationTest()
        {
            Mock<IImagesService> imagesServiceMock = new Mock<IImagesService>();
            imagesServiceMock
                .Setup(ls => ls.UploadImageAsync(new StreamMock()))
                .Returns(Task.FromResult("url"));

            using (ApplicationDbContext dbContext = serviceProvider.GetService<ApplicationDbContext>())
            {
                LocationsService locationsService = new LocationsService(dbContext,
                    serviceProvider.GetService<IMapper>(),
                    serviceProvider.GetService<IConfiguration>(),
                    imagesServiceMock.Object);

                AddLocationViewModel locationViewModel = new AddLocationViewModel
                {
                    Title = "LocationTitle",
                    Description = "LocationDescription",
                    TownId = 1,
                };

                locationsService.AddAsync(locationViewModel).GetAwaiter().GetResult();

                Location location = dbContext.Locations.First();

                Assert.Equal(locationViewModel.Title, location.Title);
            }
        }

        [Fact]
        public void AddLocationWithoutName()
        {
            Mock<IImagesService> imagesServiceMock = new Mock<IImagesService>();
            imagesServiceMock
                .Setup(ls => ls.UploadImageAsync(new StreamMock()))
                .Returns(Task.FromResult("url"));

            using (ApplicationDbContext dbContext = serviceProvider.GetService<ApplicationDbContext>())
            {
                IConfiguration configuration = serviceProvider.GetService<IConfiguration>();

                LocationsService locationsService = new LocationsService(dbContext,
                    serviceProvider.GetService<IMapper>(),
                    configuration,
                    imagesServiceMock.Object);

                AddLocationViewModel locationViewModel = new AddLocationViewModel
                {
                    Description = "LocationDescription",
                    TownId = 1,
                };

                locationsService.AddAsync(locationViewModel).GetAwaiter().GetResult();

                int locations = dbContext.Locations.CountAsync().GetAwaiter().GetResult();
                Location locationWithoutName = dbContext.Locations.FirstAsync().GetAwaiter().GetResult();

                Assert.Equal(configuration["DefaultLocationName"], locationWithoutName.Title);

                Assert.True(locations == 1);
            }
        }

        [Fact]
        public void ArticlesOrderTest()
        {
            Mock<IImagesService> imagesServiceMock = new Mock<IImagesService>();
            imagesServiceMock
                .Setup(ls => ls.UploadImageAsync(new StreamMock()))
                .Returns(Task.FromResult("url"));

            using (ApplicationDbContext dbContext = serviceProvider.GetService<ApplicationDbContext>())
            {
                Article zeroViewsArticle = new Article
                {
                    Title = "zeroViewsArticle",
                    Content = "testContent",
                    Views = 0,
                    IsApproved = true,
                };

                Article twoViewsArticle = new Article
                {
                    Title = "twoViewsArticle",
                    Content = "testContent",
                    Views = 2,
                    IsApproved = true,
                };

                dbContext.Articles.Add(zeroViewsArticle);
                dbContext.Articles.Add(twoViewsArticle);
                dbContext.SaveChangesAsync().GetAwaiter().GetResult();

                ArticlesService articlesService = new ArticlesService(dbContext,
                    serviceProvider.GetService<IMapper>(),
                    serviceProvider.GetService<IConfiguration>(),
                    imagesServiceMock.Object);

                CarWashPOI.ViewModels.Articles.ArticlesIndexOutputModel resultFromService = articlesService.GetArticlesAsync(0, 2, "views").GetAwaiter().GetResult();

                Assert.Equal(resultFromService.Articles.First().Title, twoViewsArticle.Title);
            }
        }

        [Fact]
        public void DontShowUnapprovedArticles()
        {
            Mock<IImagesService> imagesServiceMock = new Mock<IImagesService>();
            imagesServiceMock
                .Setup(ls => ls.UploadImageAsync(new StreamMock()))
                .Returns(Task.FromResult("url"));

            using (ApplicationDbContext dbContext = serviceProvider.GetService<ApplicationDbContext>())
            {
                Article article = new Article
                {
                    Title = "zeroViewsArticle",
                    Content = "testContent",
                };

                dbContext.Articles.Add(article);
                dbContext.SaveChangesAsync().GetAwaiter().GetResult();

                ArticlesService articlesService = new ArticlesService(dbContext,
                    serviceProvider.GetService<IMapper>(),
                    serviceProvider.GetService<IConfiguration>(),
                    imagesServiceMock.Object);

                CarWashPOI.ViewModels.Articles.ArticlesIndexOutputModel resultFromService = articlesService.GetArticlesAsync(0, 2, "views").GetAwaiter().GetResult();

                Assert.True(resultFromService.Articles.Count() == 0);
            }
        }
    }
}
