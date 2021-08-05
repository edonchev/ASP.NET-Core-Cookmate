namespace Cookmate.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Cookmate.Models.Recipes;
    using Cookmate.Services.Recipes;
    using Microsoft.AspNetCore.Authorization;

    public class RecipesController : Controller
    {
        private readonly IRecipeService recipeService;

        public RecipesController(IRecipeService recipeService) 
            => this.recipeService = recipeService;

        [Authorize]
        public IActionResult Add() => View(new AddRecipeFormModel
        {
            RecipeCategories = this.recipeService.GetRecipeCategories()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddRecipeFormModel recipe)
        {
            if (!this.recipeService.RecipeCategoryExists(recipe.RecipeCategoryId))
            {
                this.ModelState.AddModelError(nameof(recipe.RecipeCategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                recipe.RecipeCategories = this.recipeService.GetRecipeCategories();

                return View(recipe);
            }

            this.recipeService.AddRecipe(
                recipe.Name,
                recipe.Description,
                recipe.CookingTime,
                recipe.PictureUrl,
                recipe.RecipeCategoryId
                );

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllRecipesQueryModel query)
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
    }
}
