namespace Cookmate.Test.Api
{
    using Xunit;
    using Cookmate.Controllers.Api;
    using Cookmate.Test.Mocks;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            //Arrange
            var statisticsController = new StatisticsApiController(StatisticsSetrviceMock.Instance);

            //Act
            var result = statisticsController.GetStatistics();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.TotalRecipes);
            Assert.Equal(3, result.TotalUsers);
        }
    }
}
