namespace Cookmate.Models.Api.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AllRecipesApiRequestModel
    {
        public int RecipeCategoryId { get; init; }

        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int RecipesPerPage { get; init; } = 10;

        public RecipeSorting Sorting { get; set; }
    }
}
