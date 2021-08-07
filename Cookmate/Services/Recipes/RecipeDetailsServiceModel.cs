namespace Cookmate.Services.Recipes
{
    public class RecipeDetailsServiceModel : RecipeServiceModel
    {
        public string Description { get; init; }

        public int RecipeCategoryId { get; init; }

        public string UserId { get; init; }
    }
}
