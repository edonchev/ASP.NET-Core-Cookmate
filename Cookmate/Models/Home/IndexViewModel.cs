namespace Cookmate.Models.Home
{
    using Cookmate.Services.Recipes.Models;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalRecipes { get; init; }

        public int TotalUsers { get; set; }

        public IList<LatestRecipeServiceModel> Recipes { get; init; }
    }
}
