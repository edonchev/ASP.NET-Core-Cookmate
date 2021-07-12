namespace Cookmate.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int CookingTime { get; set; }

        public int Likes { get; set; }

        public string PictureUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

        [Required]
        public IEnumerable<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
