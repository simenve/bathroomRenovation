using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BathroomRenovation.Data
{
    public class SqliteInMemoryDb<TDbContext> : IDisposable where TDbContext : DbContext
    {
        private const string InMemoryConnectionString = "Data Source=:memory:;";
        private readonly SqliteConnection _connection;
        private readonly Func<DbContextOptions<TDbContext>, TDbContext> _contextFactory;
        private readonly DbContextOptions<TDbContext> _options;

        public SqliteInMemoryDb(Func<DbContextOptions<TDbContext>, TDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            _options = new DbContextOptionsBuilder<TDbContext>()
                .UseSqlite(_connection)
                .Options;

            CreateDbContext().Database.EnsureCreated();
        }
        public TDbContext CreateDbContext() => _contextFactory(_options);

        public SqliteConnection Connection { get { return _connection; } }

        public void Dispose() => _connection.Close();
    }
}
