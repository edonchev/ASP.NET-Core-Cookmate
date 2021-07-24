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

        public IActionResult All([FromQuery]AllRecipesQueryModel query)
        {
            var recipesQuery = this.data.Recipes.AsQueryable();

            var recipeCategories = this.GetRecipeCategories();

            if (this.data.RecipeCategories.Any(c => c.Id == query.RecipeCategoryId))
            {
                recipesQuery = recipesQuery
                    .Where(r => r.RecipeCategoryId == query.RecipeCategoryId);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                recipesQuery = recipesQuery.Where(r =>
                    r.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    r.Description.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    r.Ingredients.Any(i =>
                        i.Ingredient.Name.ToLower().Contains(query.SearchTerm.ToLower())));
            };

            recipesQuery = query.Sorting switch
            {
                RecipeSorting.MostLiked => recipesQuery.OrderByDescending(r => r.Likes).ThenBy(r => r.Name),
                RecipeSorting.IngredientsCount => recipesQuery.OrderBy(r => r.Ingredients.Count()),
                RecipeSorting.LastAdded or _=> recipesQuery.OrderByDescending(r => r.Id)
            };

            var totalRecipes = recipesQuery.Count();

            var recipes = recipesQuery
                .Skip((query.CurrentPage - 1) * AllRecipesQueryModel.RecipesPerPage)
                .Take(AllRecipesQueryModel.RecipesPerPage)
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

            query.Recipes = recipes;
            query.RecipeCategories = recipeCategories;
            query.TotalRecipes = totalRecipes;

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
