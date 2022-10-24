namespace Cookmate.Test.Conrollers
{
    using Xunit;
    using Microsoft.AspNetCore.Mvc;
    using Cookmate.Controllers;
    using Cookmate.Services.Recipes;
    using Cookmate.Test.Mocks;
    using Cookmate.Services.Statistics;
    using Cookmate.Models.Home;
    using Cookmate.Data.Models;
    using System.Linq;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            var recipes = Enumerable.Range(0, 10).Select(i => new Recipe());

            data.Recipes.AddRange(recipes);
            data.Users.Add(new User());
            data.SaveChanges();

            var recipeService = new RecipeService(data, mapper);
            var statisticsService = new StatisticsService(data);

            var homeController = new HomeController(statisticsService, recipeService);

            //Act
            var result = homeController.Index();

            //Assert
            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(3, indexViewModel.Recipes.Count);
            Assert.Equal(10, indexViewModel.TotalRecipes);
            Assert.Equal(1, indexViewModel.TotalUsers);
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController(null, null);

            //Act
            var result = homeController.Error();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
