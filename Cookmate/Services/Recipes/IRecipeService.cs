namespace Cookmate.Services.Recipes
{
    using System.Collections.Generic;
    using Cookmate.Models;

    public interface IRecipeService
    {
        RecipeQueryServiceModel All(
            int recipeCategoryId,
            string searchTerm,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage);

        IEnumerable<RecipeCategoryServiceModel> GetRecipeCategories();

        bool RecipeCategoryExists(int recipeCategoryId);

        int AddRecipe(string name,
            string description,
            int cookingTime,
            string pictureUrl,
            int recipeCategoryId);
    }
}
