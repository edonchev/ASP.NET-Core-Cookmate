namespace Cookmate.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Recipe
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(RecipeNameMax)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CookingTime { get; set; }

        public int Likes { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public int RecipeCategoryId { get; set; }

        [Required]
        public RecipeCategory RecipeCategory { get; init; }

        [Required]
        public IEnumerable<IngredientRecipe> Ingredients { get; set; } = new List<IngredientRecipe>();
    }
}
