using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cookmate.Models.Recipes
{
    public class AllRecipesQueryModel
    {
        [Display(Name = "Search by text:")]
        public string SearchTerm { get; init; }

        public IEnumerable<RecipeListingViewModel> Recipes { get; init; }
    }
}
