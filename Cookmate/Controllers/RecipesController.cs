namespace Cookmate.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Cookmate.Data;
    using Cookmate.Models.Recipes;
    using Cookmate.Data.Models;
    using Cookmate.Services.Recipes;

    public class RecipesController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly CookmateDbContext data;

        public RecipesController(IRecipeService recipeService, CookmateDbContext data)
        {
            this.recipeService = recipeService;
            this.data = data;
        }

        public IActionResult Add() => View(new AddRecipeFormModel
        {
            RecipeCategories = this.recipeService.GetRecipeCategories()
        });

        public IActionResult All([FromQuery]AllRecipesQueryModel query)
        {
            var recipeCategories = this.recipeService.GetRecipeCategories();

            var queryResult = this.recipeService
                .All(query.RecipeCategoryId,
                     query.SearchTerm,
                     query.Sorting,
                     query.CurrentPage,
                     AllRecipesQueryModel.RecipesPerPage);

            query.Recipes = queryResult.Recipes;
            query.RecipeCategories = recipeCategories;
            query.TotalRecipes = queryResult.TotalRecipes;

            return View(query);
        }

        [HttpPost]
        public IActionResult Add(AddRecipeFormModel recipe)
        {
            if (!this.data.RecipeCategories.Any(c => c.Id == recipe.RecipeCategoryId))
            {
                this.ModelState.AddModelError(nameof(recipe.RecipeCategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                recipe.RecipeCategories = this.recipeService.GetRecipeCategories();

                return View(recipe);
            }

            this.recipeService.AddRecipe(recipe);

            return RedirectToAction(nameof(All));
        }
    }
}
