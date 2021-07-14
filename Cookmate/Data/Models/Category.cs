namespace Cookmate.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }
    }
}
