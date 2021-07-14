using System.Collections.Generic;

namespace Cookmate.Data.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }

        public IEnumerable<Ingredient> Products { get; set; }
    }
}
