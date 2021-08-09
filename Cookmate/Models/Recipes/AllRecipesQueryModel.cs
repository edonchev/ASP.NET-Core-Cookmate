namespace Cookmate.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Cookmate.Services.Recipes.Models;

    public class AllRecipesQueryModel
    {
        public const int RecipesPerPage = 3;

        [Display(Name = "Search by recipe category:")]
        public int RecipeCategoryId { get; init; }

        [Display(Name = "Search by text:")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalRecipes { get; set; }

        public RecipeSorting Sorting { get; set; }

        public IEnumerable<RecipeServiceModel> Recipes { get; set; }

        public IEnumerable<RecipeCategoryServiceModel> RecipeCategories { get; set; }
    }
}
