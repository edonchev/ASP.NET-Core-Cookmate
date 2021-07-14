namespace Cookmate.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Ingredient
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(IngredientNameMax)]
        public string Name { get; set; }

        public int ShoppingListId { get; set; }

        public ShoppingList ShoppingList { get; init; }

        public int IngredientCategoryId { get; set; }

        public IngredientCategory IngredientCategory { get; init; }

        public IEnumerable<IngredientRecipe> Recipes { get; init; } = new List<IngredientRecipe>();
    }
}
