namespace Cookmate.Services.Statistics
{
    using System.Linq;
    using Cookmate.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly CookmateDbContext data;

        public StatisticsService(CookmateDbContext data) 
            => this.data = data;

        public StatisticsServiceModel Get()
        {
            var totalRecipes = this.data.Recipes.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalRecipes = totalRecipes,
                TotalUsers = totalUsers
            };
        }
    }
}
