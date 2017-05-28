using System;
using Microsoft.Extensions.Configuration;

namespace MLAccessCore.Connections {
    public class ConnectionStringProvider : IConnectionStringProvider {
        private const string _connectionname = "db:connection";
        private readonly string _connectionString;

        string IConnectionStringProvider.ConnectionString => _connectionString;

        public ConnectionStringProvider(IConfiguration configuration) {
            _connectionString = configuration[_connectionname];
        }
    }
}