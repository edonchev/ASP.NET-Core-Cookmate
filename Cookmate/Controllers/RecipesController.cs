namespace Cookmate.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Cookmate.Data;
    using Cookmate.Models.Recipes;
    using Cookmate.Data.Models;

    public class RecipesController : Controller
    {
        private readonly CookmateDbContext data;

        public RecipesController(CookmateDbContext data)
            => this.data = data;

        public IActionResult Add() => View(new AddRecipeFormModel
        {
            RecipeCategories = this.GetRecipeCategories()
        });

        public IActionResult All(string searchTerm)
        {
            var recipesQuery = this.data.Recipes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                recipesQuery = recipesQuery.Where(r =>
                    r.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    r.Description.ToLower().Contains(searchTerm.ToLower()) ||
                    r.Ingredients.Any(i =>
                        i.Ingredient.Name.ToLower().Contains(searchTerm.ToLower())));
            };

            var recipes = recipesQuery
                .OrderByDescending(r => r.Id)
                .Select(r => new RecipeListingViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    CookingTime = r.CookingTime,
                    Likes = r.Likes,
                    PictureUrl = r.PictureUrl,
                    RecipeCategory = r.RecipeCategory.Name
                })
                .ToList();

            return View(new AllRecipesQueryModel
            {
                Recipes = recipes,
                SearchTerm = searchTerm
            });
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
                recipe.RecipeCategories = this.GetRecipeCategories();

                return View(recipe);
            }

            var newRecipe = new Recipe
            {
                Name = recipe.Name,
                Description = recipe.Description,
                CookingTime = recipe.CookingTime,
                Likes = 0,
                PictureUrl = recipe.PictureUrl,
                RecipeCategoryId = recipe.RecipeCategoryId
                //Ingredients???
            };

            this.data.Recipes.Add(newRecipe);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
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
