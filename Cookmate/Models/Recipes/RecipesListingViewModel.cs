namespace Cookmate.Models.Recipes
{
    public class RecipeListingViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string PictureUrl { get; init; }

        public int CookingTime { get; set; }

        public int Likes { get; set; }

        public string RecipeCategory { get; init; }
    }
}
