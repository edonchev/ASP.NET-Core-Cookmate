namespace Cookmate.Models.Api.Recipes
{
    using System.Collections.Generic;

    public class AllRecipesApiResponseModel
    {
        public int TotalRecipes { get; init; }

        public int CurrentPage { get; init; }

        public int RecipesPerPage { get; init; }

        public IEnumerable<RecipeResponseModel> Recipes { get; init; }
    }
}
