namespace Cookmate.Services.Recipes
{
    using System.Linq;
    using Cookmate.Data;
    using Cookmate.Models;

    public class RecipeService : IRecipeService
    {
        private readonly CookmateDbContext data;

        public RecipeService(CookmateDbContext data) 
            => this.data = data;

        public RecipeQueryServiceModel All(
            int recipeCategoryId,
            string searchTerm,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage)
        {
            var recipesQuery = this.data.Recipes.AsQueryable();

            if (this.data.RecipeCategories.Any(c => c.Id == recipeCategoryId))
            {
                recipesQuery = recipesQuery
                    .Where(r => r.RecipeCategoryId == recipeCategoryId);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                recipesQuery = recipesQuery.Where(r =>
                    r.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    r.Description.ToLower().Contains(searchTerm.ToLower()) ||
                    r.Ingredients.Any(i =>
                        i.Ingredient.Name.ToLower().Contains(searchTerm.ToLower())));
            };

            recipesQuery = sorting switch
            {
                RecipeSorting.MostLiked => recipesQuery.OrderByDescending(r => r.Likes).ThenBy(r => r.Name),
                RecipeSorting.IngredientsCount => recipesQuery.OrderBy(r => r.Ingredients.Count()),
                RecipeSorting.LastAdded or _ => recipesQuery.OrderByDescending(r => r.Id)
            };

            var totalRecipes = recipesQuery.Count();

            var recipes = recipesQuery
                .Skip((currentPage - 1) * recipesPerPage)
                .Take(recipesPerPage)
                .Select(r => new RecipeServiceModel
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

            return new RecipeQueryServiceModel
            {
                TotalRecipes = totalRecipes,
                RecipesPerPage = recipesPerPage,
                CurrentPage = currentPage,
                Recipes = recipes
            };
        }
    }
}
