namespace Cookmate.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class IngredientCategory
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(CategoryNameMax)]
        public string Name { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; init; } = new List<Ingredient>();
    }
}
