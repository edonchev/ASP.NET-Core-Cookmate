namespace Cookmate.Data.Configuration
{
    using Cookmate.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> ingredient)
        {
            ingredient
                .HasOne(i => i.IngredientCategory)
                .WithMany(ic => ic.Ingredients)
                .HasForeignKey(i => i.IngredientCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            ingredient
                .HasOne(i => i.ShoppingList)
                .WithMany(s => s.Products)
                .HasForeignKey(i => i.ShoppingListId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
