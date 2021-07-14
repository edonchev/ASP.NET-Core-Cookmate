namespace Cookmate.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class RecipeCategory
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(CategoryNameMax)]
        public string Name { get; set; }

        public IEnumerable<Recipe> Recipes { get; init; } = new List<Recipe>();
    }
}
