namespace Cookmate.Controllers.Api
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Cookmate.Data;
    using Cookmate.Models.Api.Statistics;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly CookmateDbContext data;

        public StatisticsApiController(CookmateDbContext data)
            => this.data = data;

        [HttpGet]
        public StatisticsResponseModel GetStatistics()
        {
            var totalRecipes = this.data.Recipes.Count();
            var toalUsers = this.data.Users.Count();

            return new StatisticsResponseModel
            {
                TotalRecipes = totalRecipes,
                TotalUsers = toalUsers
            };
        }
    }
}
