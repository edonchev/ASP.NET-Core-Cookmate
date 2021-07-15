namespace Cookmate.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Cookmate.Data;
    using Cookmate.Models.Recipes;

    public class RecipesController : Controller
    {
        private readonly CookmateDbContext data;

        public RecipesController(CookmateDbContext data)
            => this.data = data;

        public IActionResult Add() => View(new AddRecipeFormModel
        {
            RecipeCategories = this.GetRecipeCategories()
        });

        [HttpPost]
        public IActionResult Add(AddRecipeFormModel recipe)
        {
            return View();
        }

        private IEnumerable<RecipeCategoryViewModel> GetRecipeCategories()
            => this.data
                .RecipeCategories
                .Select(rc => new RecipeCategoryViewModel
                {
                    Id = rc.Id,
                    Name = rc.Name
                })
                .ToList();
    }
}
