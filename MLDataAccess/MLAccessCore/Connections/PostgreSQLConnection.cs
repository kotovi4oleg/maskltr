using System.Data;
using Npgsql;

namespace MLAccessCore.Connections {
    public class PostgreSQLConnection : IConnection {
        private readonly IConnectionStringProvider _connectionStringProvider;
        public PostgreSQLConnection(IConnectionStringProvider connectionString) {
            _connectionStringProvider = connectionString;
        }

        public IDbConnection Connection => new NpgsqlConnection(_connectionStringProvider.ConnectionString);
    }
}