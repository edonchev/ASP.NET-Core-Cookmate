namespace Cookmate.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Cookmate.Models;
    using Cookmate.Data;
    using Cookmate.Models.Home;
    using Cookmate.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly CookmateDbContext data;
        private readonly IStatisticsService statistics;

        public HomeController(
            CookmateDbContext data,
            IStatisticsService statistics)
        {
            this.data = data;
            this.statistics = statistics;
        }

        public IActionResult Index()
        {
            var recipes = this.data
                .Recipes
                .OrderByDescending(r => r.Id)
                .Select(r => new RecipeIndexViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    PictureUrl = r.PictureUrl,
                    Likes = r.Likes
                })
                .Take(3)
                .ToList();

            var totalStatistics = this.statistics.Get();

            return View(new IndexViewModel 
            {
                TotalRecipes = totalStatistics.TotalRecipes,
                TotalUsers = totalStatistics.TotalUsers,
                Recipes = recipes
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
