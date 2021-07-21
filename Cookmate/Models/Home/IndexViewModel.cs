using System.Collections.Generic;

namespace Cookmate.Models.Home
{
    public class IndexViewModel
    {
        public int TotalRecipes { get; init; }

        public int TotalUsers { get; set; }

        public List<RecipeIndexViewModel> Recipes { get; init; }
    }
}
