using CarWashPOI.Controllers;
using CarWashPOI.Data.Models;
using CarWashPOI.Services.Locations;
using CarWashPOI.Services.LocationTypes;
using CarWashPOI.Services.Towns;
using CarWashPOI.ViewModels;
using CarWashPOI.ViewModels.Locations;
using CarWashPOI.ViewModels.LocationTypes;
using CarWashPOI.ViewModels.Towns;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarWashPOITests
{

    public class ControllersTests
    {
        [Fact]
        public void IndexTestWith0Results()
        {
            var locationsServiceMock = new Mock<ILocationsService>();
            locationsServiceMock
                .Setup(ls => ls.GetLocationsAsync(0, 0, null, 9, 9))
                .Returns(Task.FromResult(new HomePageOutputModel()));

            var townsServiceMock = new Mock<ITownsService>();
            townsServiceMock
                .Setup(ts => ts.GetAllTownsAsync())
                .Returns(Task.FromResult(new TownViewModel[0]));

            var locationTypesServiceMock = new Mock<ILocationTypesService>();
            locationTypesServiceMock
                .Setup(lts => lts.GetAllLocationTypesAsync())
                .Returns(Task.FromResult(new LocationTypeViewModel[0]));

            var homeController = new HomeController(locationsServiceMock.Object, townsServiceMock.Object, locationTypesServiceMock.Object);

            var result = homeController.Index(new HomePageInputModel { Page = 2 }).GetAwaiter().GetResult();

            var viewResult = result as ViewResult;
            var viewModel = viewResult.Model as HomePageOutputModel;

            Assert.True(viewModel.AllLocations == 0);
            Assert.True(viewModel.AllTowns.Count() == 0);
            Assert.True(viewModel.AllTypes.Count() == 0);
            Assert.True(viewModel.CurrentPage == 0);
            Assert.True(viewModel.LastPage == 0);
        }

        [Fact]
        public void IndexTestWith11Results()
        {
            var locationsServiceMock = new Mock<ILocationsService>();
            locationsServiceMock
                .Setup(ls => ls.GetLocationsAsync(0, 0, null, 9, 9))
                .Returns(Task.FromResult(new HomePageOutputModel
                {
                    AllLocations = 11,
                }));

            var townsServiceMock = new Mock<ITownsService>();
            townsServiceMock
                .Setup(ts => ts.GetAllTownsAsync())
                .Returns(Task.FromResult(new TownViewModel[11]));

            var locationTypesServiceMock = new Mock<ILocationTypesService>();
            locationTypesServiceMock
                .Setup(lts => lts.GetAllLocationTypesAsync())
                .Returns(Task.FromResult(new LocationTypeViewModel[11]));

            var homeController = new HomeController(locationsServiceMock.Object, townsServiceMock.Object, locationTypesServiceMock.Object);

            var result = homeController.Index(new HomePageInputModel { Page = 2 }).GetAwaiter().GetResult();

            var viewResult = result as ViewResult;
            var viewModel = viewResult.Model as HomePageOutputModel;

            Assert.True(viewModel.AllLocations == 11);
            Assert.True(viewModel.AllTowns.Count() == 11);
            Assert.True(viewModel.AllTypes.Count() == 11);
            Assert.True(viewModel.CurrentPage == 2);
            Assert.True(viewModel.LastPage == 2);
        }

        [Fact]
        public void PagingTest()
        {
            var locationsServiceMock = new Mock<ILocationsService>();
            locationsServiceMock
                .Setup(ls => ls.GetLocationsAsync(0, 0, null, 36, 9))
                .Returns(Task.FromResult(new HomePageOutputModel
                {
                    AllLocations = 11,
                }));

            var townsServiceMock = new Mock<ITownsService>();
            townsServiceMock
                .Setup(ts => ts.GetAllTownsAsync())
                .Returns(Task.FromResult(new TownViewModel[11]));

            var locationTypesServiceMock = new Mock<ILocationTypesService>();
            locationTypesServiceMock
                .Setup(lts => lts.GetAllLocationTypesAsync())
                .Returns(Task.FromResult(new LocationTypeViewModel[11]));

            var homeController = new HomeController(locationsServiceMock.Object, townsServiceMock.Object, locationTypesServiceMock.Object);

            var result = homeController.Index(new HomePageInputModel { Page = 5 }).GetAwaiter().GetResult();

            Assert.IsType<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;

            var actionName = redirectResult.ActionName;
            var page = redirectResult.RouteValues["Page"] as int?;

            Assert.Equal("Index", actionName);
            Assert.Equal(2, page);
        }
    }
}
