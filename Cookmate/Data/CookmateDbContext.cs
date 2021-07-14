namespace Cookmate.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CookmateDbContext : IdentityDbContext
    {
        public CookmateDbContext(DbContextOptions<CookmateDbContext> options)
            : base(options)
        {
        }
    }
}
