namespace Cookmate.Services.Recipes
{
    using Cookmate.Models;
    public interface IRecipeService
    {
        RecipeQueryServiceModel All(
            int recipeCategoryId,
            string searchTerm,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage);
    }
}
