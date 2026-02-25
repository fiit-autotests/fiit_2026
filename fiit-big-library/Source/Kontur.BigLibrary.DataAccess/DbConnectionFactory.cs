using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Kontur.BigLibrary.DataAccess
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly SqliteConnectionStringBuilder connectionStringBuilder;

        public DbConnectionFactory(string connectionString)
        {
            this.connectionStringBuilder = new SqliteConnectionStringBuilder(connectionString);
        }

        public async Task<IDbConnection> OpenAsync(CancellationToken cancellation)
        {
            var sqliteConnection = new SqliteConnection(connectionStringBuilder.ConnectionString);
            await sqliteConnection.OpenAsync(cancellation);
            return sqliteConnection;
        }
    }
}