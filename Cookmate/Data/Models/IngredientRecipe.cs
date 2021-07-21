namespace Cookmate.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class IngredientRecipe
    {
        [Required]
        public int IngredientId { get; set; }

        [Required]
        public int RecipeId { get; set; }

        [Required]
        public Ingredient Ingredient { get; set; }

        [Required]
        public Recipe Recipe { get; set; }

        [Required]
        [MaxLength(IngredientQuantityMax)]
        public double IngredientQuantity { get; set; }

        [Required]
        [MaxLength(MeasurementUnitMaxLength)]
        public string MeasurementUnit { get; set; }
    }
}
