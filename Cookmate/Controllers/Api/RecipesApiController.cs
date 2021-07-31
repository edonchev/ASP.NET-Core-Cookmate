namespace Cookmate.Controllers.Api
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Cookmate.Data;
    using Cookmate.Models.Api.Recipes;
    using Cookmate.Models;

    [ApiController]
    [Route("api/recipes")]
    public class RecipesApiController : ControllerBase
    {
        private readonly CookmateDbContext data;

        public RecipesApiController(CookmateDbContext data)
            => this.data = data;


        [HttpGet]
        public ActionResult<AllRecipesApiResponseModel> All([FromQuery] AllRecipesApiRequestModel query)
        {
            var recipesQuery = this.data.Recipes.AsQueryable();

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
                RecipeSorting.LastAdded or _ => recipesQuery.OrderByDescending(r => r.Id)
            };

            var totalRecipes = recipesQuery.Count();

            var recipes = recipesQuery
                .Skip((query.CurrentPage - 1) * query.RecipesPerPage)
                .Take(query.RecipesPerPage)
                .Select(r => new RecipeResponseModel
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

            return new AllRecipesApiResponseModel
            {
                TotalRecipes = totalRecipes,
                CurrentPage = query.CurrentPage,
                RecipesPerPage = query.RecipesPerPage,
                Recipes = recipes
            };
        }
    }
}
