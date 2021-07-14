namespace Cookmate.Data
{
    using Cookmate.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public class CookmateDbContext : IdentityDbContext
    {
        public CookmateDbContext(DbContextOptions<CookmateDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; init; }

        public DbSet<RecipeCategory> RecipeCategories { get; init; }

        public DbSet<Ingredient> Ingredients { get; init; }

        public DbSet<IngredientCategory> IngredientCategories { get; init; }

        public DbSet<ShoppingList> ShoppingLists { get; init; }

        public DbSet<IngredientRecipe> IngredientsRecipes { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            base.OnModelCreating(builder);
        }
    }
}
