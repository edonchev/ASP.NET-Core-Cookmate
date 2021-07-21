namespace Cookmate.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class AddRecipeFormModel
    {
        [Required]
        [StringLength(RecipeNameMax, MinimumLength = RecipeNameMin)]
        public string Name { get; init; }

        [Required]
        [StringLength(int.MaxValue, 
            MinimumLength = RecipeDescriptionMin,
            ErrorMessage = "Описанието трябва да е с мин. дължина {2} символа!")]
        public string Description { get; init; }

        [Required]
        [Display(Name = "Cooking Time in min")]
        [Range(CookingTimeMin, 
            CookingTimeMax, 
            ErrorMessage = "Необходимото време нa приготвяне може да е между {1} и {2} min.")]
        public int CookingTime { get; init; }

        [Required]
        [Display(Name = "Picture URL")]
        [Url]
        public string PictureUrl { get; init; }

        [Required]
        [Display(Name = "Category")]
        public int RecipeCategoryId { get; init; }

        public IEnumerable<RecipeCategoryViewModel> RecipeCategories { get; set; }
    }
}
