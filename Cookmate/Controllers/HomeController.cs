namespace Cookmate.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Cookmate.Models;
    using Cookmate.Data;
    using Cookmate.Models.Home;

    public class HomeController : Controller
    {
        private readonly CookmateDbContext data;

        public HomeController(CookmateDbContext data) 
            => this.data = data;

        public IActionResult Index()
        {
            var totalRecipes = this.data.Recipes.Count();
            var totalUsers = this.data.Users.Count();

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

            return View(new IndexViewModel 
            {
                TotalRecipes = totalRecipes,
                TotalUsers = totalUsers,
                Recipes = recipes
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
