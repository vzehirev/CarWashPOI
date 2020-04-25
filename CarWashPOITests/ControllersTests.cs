using CarWashPOI.Controllers;
using CarWashPOI.Services.Locations;
using CarWashPOI.Services.LocationTypes;
using CarWashPOI.Services.Towns;
using CarWashPOI.ViewModels;
using CarWashPOI.ViewModels.LocationTypes;
using CarWashPOI.ViewModels.Towns;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CarWashPOITests
{

    public class ControllersTests
    {
        [Fact]
        public void IndexTestWith0Results()
        {
            Mock<ILocationsService> locationsServiceMock = new Mock<ILocationsService>();
            locationsServiceMock
                .Setup(ls => ls.GetLocationsAsync(0, 0, null, 9, 9))
                .Returns(Task.FromResult(new HomePageOutputModel()));

            Mock<ITownsService> townsServiceMock = new Mock<ITownsService>();
            townsServiceMock
                .Setup(ts => ts.GetAllTownsAsync())
                .Returns(Task.FromResult(new TownViewModel[0]));

            Mock<ILocationTypesService> locationTypesServiceMock = new Mock<ILocationTypesService>();
            locationTypesServiceMock
                .Setup(lts => lts.GetAllLocationTypesAsync())
                .Returns(Task.FromResult(new LocationTypeViewModel[0]));

            HomeController homeController = new HomeController(locationsServiceMock.Object, townsServiceMock.Object, locationTypesServiceMock.Object);

            IActionResult result = homeController.Index(new HomePageInputModel { Page = 2 }).GetAwaiter().GetResult();

            ViewResult viewResult = result as ViewResult;
            HomePageOutputModel viewModel = viewResult.Model as HomePageOutputModel;

            Assert.True(viewModel.AllLocations == 0);
            Assert.True(viewModel.AllTowns.Count() == 0);
            Assert.True(viewModel.AllTypes.Count() == 0);
            Assert.True(viewModel.CurrentPage == 0);
            Assert.True(viewModel.LastPage == 0);
        }

        [Fact]
        public void IndexTestWith11Results()
        {
            Mock<ILocationsService> locationsServiceMock = new Mock<ILocationsService>();
            locationsServiceMock
                .Setup(ls => ls.GetLocationsAsync(0, 0, null, 9, 9))
                .Returns(Task.FromResult(new HomePageOutputModel
                {
                    AllLocations = 11,
                }));

            Mock<ITownsService> townsServiceMock = new Mock<ITownsService>();
            townsServiceMock
                .Setup(ts => ts.GetAllTownsAsync())
                .Returns(Task.FromResult(new TownViewModel[11]));

            Mock<ILocationTypesService> locationTypesServiceMock = new Mock<ILocationTypesService>();
            locationTypesServiceMock
                .Setup(lts => lts.GetAllLocationTypesAsync())
                .Returns(Task.FromResult(new LocationTypeViewModel[11]));

            HomeController homeController = new HomeController(locationsServiceMock.Object, townsServiceMock.Object, locationTypesServiceMock.Object);

            IActionResult result = homeController.Index(new HomePageInputModel { Page = 2 }).GetAwaiter().GetResult();

            ViewResult viewResult = result as ViewResult;
            HomePageOutputModel viewModel = viewResult.Model as HomePageOutputModel;

            Assert.True(viewModel.AllLocations == 11);
            Assert.True(viewModel.AllTowns.Count() == 11);
            Assert.True(viewModel.AllTypes.Count() == 11);
            Assert.True(viewModel.CurrentPage == 2);
            Assert.True(viewModel.LastPage == 2);
        }

        [Fact]
        public void PagingTest()
        {
            Mock<ILocationsService> locationsServiceMock = new Mock<ILocationsService>();
            locationsServiceMock
                .Setup(ls => ls.GetLocationsAsync(0, 0, null, 36, 9))
                .Returns(Task.FromResult(new HomePageOutputModel
                {
                    AllLocations = 11,
                }));

            Mock<ITownsService> townsServiceMock = new Mock<ITownsService>();
            townsServiceMock
                .Setup(ts => ts.GetAllTownsAsync())
                .Returns(Task.FromResult(new TownViewModel[11]));

            Mock<ILocationTypesService> locationTypesServiceMock = new Mock<ILocationTypesService>();
            locationTypesServiceMock
                .Setup(lts => lts.GetAllLocationTypesAsync())
                .Returns(Task.FromResult(new LocationTypeViewModel[11]));

            HomeController homeController = new HomeController(locationsServiceMock.Object, townsServiceMock.Object, locationTypesServiceMock.Object);

            IActionResult result = homeController.Index(new HomePageInputModel { Page = 5 }).GetAwaiter().GetResult();

            Assert.IsType<RedirectToActionResult>(result);
            RedirectToActionResult redirectResult = result as RedirectToActionResult;

            string actionName = redirectResult.ActionName;
            int? page = redirectResult.RouteValues["Page"] as int?;

            Assert.Equal("Index", actionName);
            Assert.Equal(2, page);
        }
    }
}
