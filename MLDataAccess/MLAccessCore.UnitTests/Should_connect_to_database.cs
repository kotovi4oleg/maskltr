using System;
using Xunit;
using MLAccessCore.Connections;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace MLAccessCore.UnitTests {
    public class Should_connect_to_database {
        private IConnectionStringProvider _connectionStringProvider;
        private IConnection _connection;

        const string ConnectionString = @"
        User ID=postgres;
        Password=postgres;
        Host=localhost;
        Port=5432;
        Database=postgres;
        Pooling=true;";

        [Fact]
        public void When_Connection_String_Is_Valid() {

            //Arrange.
            var connectionStringMock = new Mock<IConnectionStringProvider>();
            connectionStringMock
                .Setup(csp => csp.ConnectionString)
                .Returns(ConnectionString);
            //Act.
            _connectionStringProvider = connectionStringMock.Object;
            _connection = new PostgreSQLConnection(_connectionStringProvider);
            try { using(var connection = _connection.Connection){ 
                connection.Open();  
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select * from pg_stat_activity where datname = 'postgres'";
                using(var reader = cmd.ExecuteReader()) {
                    while(reader.Read()) {
                        Console.WriteLine("{0}\n",string.Join("\n", 
                        Enumerable.Range(0, reader.FieldCount)
                        .Select(reader.GetName)));
                    }
                }
            } }
            //Assert.
            catch(Exception exception){ Assert.False(true, exception.Message); }

            Assert.True(true, "Connection successfull");
        }
    }
}
