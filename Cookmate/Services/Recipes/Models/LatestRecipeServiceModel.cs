namespace Cookmate.Services.Recipes.Models
{
    public class LatestRecipeServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string PictureUrl { get; init; }

        public int Likes { get; init; }
    }
}
