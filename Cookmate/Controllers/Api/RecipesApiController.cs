namespace Cookmate.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using Cookmate.Models.Api.Recipes;
    using Cookmate.Services.Recipes;
    using Cookmate.Services.Recipes.Models;

    [ApiController]
    [Route("api/recipes")]
    public class RecipesApiController : ControllerBase
    {
        private readonly IRecipeService recipes;

        public RecipesApiController(IRecipeService recipes)
            => this.recipes = recipes;

        [HttpGet]
        public RecipeQueryServiceModel All([FromQuery] AllRecipesApiRequestModel query) 
            => this.recipes.All(
                query.RecipeCategoryId,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.RecipesPerPage);
    }
}
