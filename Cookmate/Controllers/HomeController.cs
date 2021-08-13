namespace Cookmate.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Cookmate.Models.Home;
    using Cookmate.Services.Statistics;
    using Cookmate.Services.Recipes;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IRecipeService recipeServices;

        public HomeController(
            IStatisticsService statistics,
            IRecipeService services)
        {
            this.statistics = statistics;
            this.recipeServices = services;
        }

        public IActionResult Index()
        {
            var latestRecipes = this.recipeServices
                .Latest()
                .ToList();

            var totalStatistics = this.statistics.Get();

            return View(new IndexViewModel 
            {
                TotalRecipes = totalStatistics.TotalRecipes,
                TotalUsers = totalStatistics.TotalUsers,
                Recipes = latestRecipes
            });
        }

        public IActionResult Error() => View();
    }
}
