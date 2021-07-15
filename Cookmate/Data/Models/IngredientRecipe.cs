namespace Cookmate.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class IngredientRecipe
    {
        public int IngredientId { get; set; }

        public int RecipeId { get; set; }

        public Ingredient Ingredient { get; set; }

        public Recipe Recipe { get; set; }

        public double IngredientQuantity { get; set; }

        [MaxLength(MeasurementUnitMaxLength)]
        public string MeasurementUnit { get; set; }
    }
}
