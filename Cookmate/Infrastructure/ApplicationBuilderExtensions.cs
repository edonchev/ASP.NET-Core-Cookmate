using Microsoft.AspNetCore.Builder;

namespace Cookmate.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            //app.ApplicationServices.GetService<CookmateDbContext>().Database.Migrate();

            return app;
        }
    }
}
