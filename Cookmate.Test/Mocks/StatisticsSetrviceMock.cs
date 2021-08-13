namespace Cookmate.Test.Mocks
{
    using Moq;
    using Cookmate.Services.Statistics;

    public class StatisticsSetrviceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Get())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalRecipes = 5,
                        TotalUsers = 3
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
