namespace Cookmate.Services.Recipes
{
    using System.Collections.Generic;
    using Cookmate.Models;
    using Cookmate.Services.Recipes.Models;

    public interface IRecipeService
    {
        RecipeQueryServiceModel All(
            int recipeCategoryId,
            string searchTerm,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage);

        RecipeDetailsServiceModel Details(int recipeId);

        IEnumerable<RecipeServiceModel> ByUser(string userId);

        IEnumerable<RecipeCategoryServiceModel> GetRecipeCategories();

        bool RecipeCategoryExists(int recipeCategoryId);

        int AddRecipe(string name,
            string description,
            int cookingTime,
            string pictureUrl,
            int recipeCategoryId,
            string userId);

        bool EditRecipe(int recipeId,
            string name,
            string description,
            int cookingTime,
            string pictureUrl,
            int recipeCategoryId);
    }
}
