namespace Cookmate.Services.Users
{
    public interface IUserService
    {
        bool IsRecipeOwner(int recipeId, string userId);
    }
}
