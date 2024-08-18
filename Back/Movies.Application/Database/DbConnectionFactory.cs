using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Database {
    public interface IDbConnectionFactory {
        Task<IDbConnection> CreateConnectionAsync();
    }

    public class NpgsqlConnectionFactory : IDbConnectionFactory {
        private readonly string _connectionString;
        public NpgsqlConnectionFactory(string connectionString) {
            _connectionString = connectionString;

        }
        public async Task<IDbConnection> CreateConnectionAsync() {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
