namespace Cookmate.Services.Users
{
    using System.Linq;
    using Cookmate.Areas.Admin;
    using Cookmate.Data;

    public class UserService : IUserService
    {
        private readonly CookmateDbContext data;

        public UserService(CookmateDbContext data)
            => this.data = data;

        public bool IsRecipeOwner(int recipeId, string userId)
            => this.data
                .Recipes
                .Any(r => r.Id == recipeId && r.UserId == userId);
    }
}
