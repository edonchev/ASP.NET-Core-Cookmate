namespace Cookmate.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddRecipeFormModel
    {
        public string Name { get; init; }

        public string Description { get; init; }

        [Display(Name = "Cooking Time")]
        public int CookingTime { get; init; }

        [Display(Name = "Picture URL")]
        public string PictureUrl { get; init; }

        [Display(Name = "Category")]
        public int RecipeCategoryId { get; init; }

        public IEnumerable<RecipeCategoryViewModel> RecipeCategories { get; set; }
    }
}
