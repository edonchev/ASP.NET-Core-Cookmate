namespace Cookmate.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Cookmate.Models.Recipes;
    using Cookmate.Services.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Cookmate.Infrastructure;
    using Cookmate.Services.Users;
    using AutoMapper;

    public class RecipesController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public RecipesController(
            IRecipeService recipeService,
            IUserService userService, 
            IMapper mapper)
        {
            this.recipeService = recipeService;
            this.userService = userService;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Add() => View(new RecipeFormModel
        {
            RecipeCategories = this.recipeService.GetRecipeCategories()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Add(RecipeFormModel recipe)
        {
            if (!this.recipeService.RecipeCategoryExists(recipe.RecipeCategoryId))
            {
                this.ModelState.AddModelError(nameof(recipe.RecipeCategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                recipe.RecipeCategories = this.recipeService.GetRecipeCategories();

                return View(recipe);
            }

            var userId = this.User.GetId();

            this.recipeService.AddRecipe(
                recipe.Name,
                recipe.Description,
                recipe.CookingTime,
                recipe.PictureUrl,
                recipe.RecipeCategoryId,
                userId
                );

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Mine()
        {
            var userRecipes = this.recipeService.ByUser(this.User.GetId());

            return View(userRecipes);
        }

        public IActionResult All([FromQuery] AllRecipesQueryModel query)
        {
            var recipeCategories = this.recipeService.GetRecipeCategories();

            var queryResult = this.recipeService
                .All(query.RecipeCategoryId,
                     query.SearchTerm,
                     query.Sorting,
                     query.CurrentPage,
                     AllRecipesQueryModel.RecipesPerPage);

            query.Recipes = queryResult.Recipes;
            query.RecipeCategories = recipeCategories;
            query.TotalRecipes = queryResult.TotalRecipes;

            return View(query);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();
            var recipe = this.recipeService.Details(id);

            if (recipe.UserId != userId)
            {
                return Unauthorized();
            }

            var recipeForm = this.mapper.Map<RecipeFormModel>(recipe);
            recipeForm.RecipeCategories = this.recipeService.GetRecipeCategories();

            return View(recipeForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, RecipeFormModel recipe)
        {
            var userCanEdit = this.userService
                    .IsRecipeOwner(id, this.User.GetId());

            if (!userCanEdit && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            if (!this.recipeService.RecipeCategoryExists(recipe.RecipeCategoryId))
            {
                this.ModelState.AddModelError(nameof(recipe.RecipeCategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                recipe.RecipeCategories = this.recipeService.GetRecipeCategories();

                return View(recipe);
            }

            var edited = this.recipeService.EditRecipe(
                id,
                recipe.Name,
                recipe.Description,
                recipe.CookingTime,
                recipe.PictureUrl,
                recipe.RecipeCategoryId
                );

            if (!edited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
