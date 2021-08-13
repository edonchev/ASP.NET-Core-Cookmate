namespace Cookmate.Infrastructure
{
    using AutoMapper;
    using Cookmate.Data.Models;
    using Cookmate.Models.Recipes;
    using Cookmate.Services.Recipes.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<RecipeDetailsServiceModel, RecipeFormModel>();
            this.CreateMap<Recipe, LatestRecipeServiceModel>();
            this.CreateMap<Recipe, RecipeDetailsServiceModel>()
                .ForMember(r => r.RecipeCategory, cfg => cfg.MapFrom(r => r.RecipeCategory.Name));
        }
    }
}
