namespace Cookmate.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Identity;
    using Cookmate.Data;
    using Cookmate.Data.Models;
    using static Cookmate.Areas.Admin.AdminConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedRecipeCategories(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<CookmateDbContext>();

            data.Database.Migrate();
        }

        private static void SeedRecipeCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<CookmateDbContext>();

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

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@cookmate.com";
                    const string adminPassword = "%Password1";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
