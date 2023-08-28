using BathroomRenovation.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BathroomRenovation.Tests
{
    public static class TestHelper
    {
        internal static async Task<TestWebApplicationFactory<Program>> WebAppFactoryWithInMemoryDb(
            IServiceCollection? services = default,
            Action<BathroomRenovationDbContext>? seedfunction = default
        )
        {
            services ??= new ServiceCollection();
            await InMemoryDbWithSeedData(services, seedfunction);

            return new TestWebApplicationFactory<Program>(services);
        }

        private static async Task InMemoryDbWithSeedData(IServiceCollection services, Action<BathroomRenovationDbContext>? seedFunction = default)
        {
            var db = new SqliteInMemoryDb<BathroomRenovationDbContext>(opt => new BathroomRenovationDbContext(opt));

            // Inmemory Sqlite is transient by default, it ceases to exist as soon as the database connection is closed
            // However EF Core's DbContext opens/closes connections automatically, unless an already open connection is provided
            // This is not a thread safe solution
            services.AddDbContext<BathroomRenovationDbContext>((provider, options) =>
            {
                options.UseSqlite(db.Connection);
            });

            var context = services.BuildServiceProvider().GetRequiredService<BathroomRenovationDbContext>();

            context.Database.EnsureCreated();

            seedFunction?.Invoke(context);

            await context.SaveChangesAsync();
        }
    }
}
