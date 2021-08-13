namespace Cookmate.Test.Mocks
{
    using Cookmate.Data;
    using Microsoft.EntityFrameworkCore;
    using System;

    public static class DatabaseMock
    {
        public static CookmateDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<CookmateDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new CookmateDbContext(dbContextOptions);
            }
        }
    }
}
