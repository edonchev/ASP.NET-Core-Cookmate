using Cookmate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookmate.Data.Configuration
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> recipe)
        {
            recipe
                .HasOne(r => r.RecipeCategory)
                .WithMany(rc => rc.Recipes)
                .HasForeignKey(r => r.RecipeCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            recipe
                .HasOne(r => r.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
