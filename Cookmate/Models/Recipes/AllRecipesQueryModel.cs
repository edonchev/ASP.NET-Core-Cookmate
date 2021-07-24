﻿namespace Cookmate.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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

        public IEnumerable<RecipeListingViewModel> Recipes { get; set; }

        public IEnumerable<RecipeCategoryViewModel> RecipeCategories { get; set; }
    }
}
