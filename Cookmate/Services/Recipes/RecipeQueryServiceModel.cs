namespace Cookmate.Services.Recipes
{
    using System.Collections.Generic;

    public class RecipeQueryServiceModel
    {
        public int TotalRecipes { get; init; }

        public int CurrentPage { get; init; }

        public int RecipesPerPage { get; init; }

        public IEnumerable<RecipeServiceModel> Recipes { get; init; }
    }
}
