namespace Cookmate.Infrastructure
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Cookmate.Data;
    using Cookmate.Data.Models;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<CookmateDbContext>();

            data.Database.Migrate();

            SeedRecipeCategories(data);

            return app;
        }

        private static void SeedRecipeCategories(CookmateDbContext data)
        {
            if (data.RecipeCategories.Any())
            {
                return;
            }

            data.RecipeCategories.AddRange(new[]
            {
                new RecipeCategory { Name = "Салати" },
                new RecipeCategory { Name = "Предястия" },
                new RecipeCategory { Name = "Супи и чорби" },
                new RecipeCategory { Name = "Постни ястия" },
                new RecipeCategory { Name = "Ястия с месо" },
                new RecipeCategory { Name = "Риба и морски продукти" },
                new RecipeCategory { Name = "Ястия с птици" },
                new RecipeCategory { Name = "Ястия с яйца" },
                new RecipeCategory { Name = "Ястия с гъби" },
                new RecipeCategory { Name = "Мляко и млечни храни" },
                new RecipeCategory { Name = "Сосове" },
                new RecipeCategory { Name = "Тестени печива"},
                new RecipeCategory { Name = "Без глутен"},
                new RecipeCategory { Name = "Десерти"},
                new RecipeCategory { Name = "Зимнина"}
            }) ;

            data.SaveChanges();
        }
    }
}
