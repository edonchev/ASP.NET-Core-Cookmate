﻿namespace Cookmate.Services.Recipes
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Cookmate.Data;
    using Cookmate.Data.Models;
    using Cookmate.Models;
    using Cookmate.Services.Recipes.Models;

    public class RecipeService : IRecipeService
    {
        private readonly CookmateDbContext data;
        private readonly IConfigurationProvider mapper;

        public RecipeService(CookmateDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public RecipeQueryServiceModel All(
            int recipeCategoryId,
            string searchTerm,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage)
        {
            var recipesQuery = this.data.Recipes.AsQueryable();

            if (this.data.RecipeCategories.Any(c => c.Id == recipeCategoryId))
            {
                recipesQuery = recipesQuery
                    .Where(r => r.RecipeCategoryId == recipeCategoryId);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                recipesQuery = recipesQuery.Where(r =>
                    r.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    r.Description.ToLower().Contains(searchTerm.ToLower()) ||
                    r.Ingredients.Any(i =>
                        i.Ingredient.Name.ToLower().Contains(searchTerm.ToLower())));
            };

            recipesQuery = sorting switch
            {
                RecipeSorting.MostLiked => recipesQuery.OrderByDescending(r => r.Likes).ThenBy(r => r.Name),
                RecipeSorting.IngredientsCount => recipesQuery.OrderBy(r => r.Ingredients.Count()),
                RecipeSorting.LastAdded or _ => recipesQuery.OrderByDescending(r => r.Id)
            };

            var totalRecipes = recipesQuery.Count();

            var recipes = GetRecipes(recipesQuery
                .Skip((currentPage - 1) * recipesPerPage)
                .Take(recipesPerPage));

            return new RecipeQueryServiceModel
            {
                TotalRecipes = totalRecipes,
                RecipesPerPage = recipesPerPage,
                CurrentPage = currentPage,
                Recipes = recipes
            };
        }

        public RecipeDetailsServiceModel Details(int recipeId)
            => this.data
                .Recipes
                .Where(r => r.Id == recipeId)
                .ProjectTo<RecipeDetailsServiceModel>(this.mapper)
                .FirstOrDefault();

        public IEnumerable<RecipeServiceModel> ByUser(string userId)
            => GetRecipes(this.data
                .Recipes
                .Where(c => c.UserId == userId));


        public IEnumerable<RecipeCategoryServiceModel> GetRecipeCategories()
            => this.data
                .RecipeCategories
                .Select(rc => new RecipeCategoryServiceModel
                {
                    Id = rc.Id,
                    Name = rc.Name
                })
                .ToList();

        public int AddRecipe(string name,
                string description,
                int cookingTime,
                string pictureUrl,
                int recipeCategoryId,
                string userId)
        {
            var newRecipe = new Recipe
            {
                Name = name,
                Description = description,
                CookingTime = cookingTime,
                PictureUrl = pictureUrl,
                RecipeCategoryId = recipeCategoryId,
                Likes = 0,
                UserId = userId
                //Ingredients???
            };

            this.data.Recipes.Add(newRecipe);
            this.data.SaveChanges();

            return newRecipe.Id;
        }

        public bool EditRecipe(int recipeId,
                string name,
                string description,
                int cookingTime,
                string pictureUrl,
                int recipeCategoryId)
        {
            var recipeData = this.data.Recipes.Find(recipeId);

            if (recipeData == null)
            {
                return false;
            }

            recipeData.Name = name;
            recipeData.Description = description;
            recipeData.CookingTime = cookingTime;
            recipeData.PictureUrl = pictureUrl;
            recipeData.RecipeCategoryId = recipeCategoryId;
            //Ingredients???

            this.data.SaveChanges();

            return true;
        }

        public bool RecipeCategoryExists(int recipeCategoryId)
            => this.data
            .RecipeCategories
            .Any(c => c.Id == recipeCategoryId);

        private static IEnumerable<RecipeServiceModel> GetRecipes(IQueryable<Recipe> recipeQuery)
            => recipeQuery
                .Select(r => new RecipeServiceModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    CookingTime = r.CookingTime,
                    Likes = r.Likes,
                    PictureUrl = r.PictureUrl,
                    RecipeCategory = r.RecipeCategory.Name
                })
                .ToList();

        public IEnumerable<LatestRecipeServiceModel> Latest() 
            => this.data
                .Recipes
                .OrderByDescending(r => r.Id)
                .ProjectTo<LatestRecipeServiceModel>(this.mapper)
                .Take(3)
                .ToList();

        public void DeleteRecipe(int id)
        {
            var recipeToDelete = this.data.Recipes.Find(id);
            this.data.Recipes.Remove(recipeToDelete);
            this.data.SaveChanges();
        }
    }
}
