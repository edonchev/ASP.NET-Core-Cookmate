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
    }
}
