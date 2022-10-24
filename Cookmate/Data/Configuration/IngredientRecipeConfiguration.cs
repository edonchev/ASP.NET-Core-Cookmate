namespace Cookmate.Data.Configuration
{
    using Cookmate.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class IngredientRecipeConfiguration : IEntityTypeConfiguration<IngredientRecipe>
    {
        public void Configure(EntityTypeBuilder<IngredientRecipe> builder)
        {
            builder.HasKey(ir => new { ir.IngredientId, ir.RecipeId });

            builder
                .HasOne(i => i.Ingredient)
                .WithMany(r => r.Recipes)
                .HasForeignKey(i => i.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(r => r.Recipe)
                .WithMany(i => i.Ingredients)
                .HasForeignKey(r => r.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
