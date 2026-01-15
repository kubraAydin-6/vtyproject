using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Persistence.Dappers
{
    public class NpgSqlHelper : INpgDbHelper
    {
        private readonly string _connectionString;

        public NpgSqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }
        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public NpgsqlCommand CreateCommand(string sql, NpgsqlConnection connection)
        {
            return new NpgsqlCommand(sql, connection);
        }

        public async Task<int> ExecuteAsync(string sql, object? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            if (transaction != null)
            {
                return await transaction.Connection!.ExecuteAsync(new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken));
            }
            await using NpgsqlConnection connection = GetConnection();
            return await connection.ExecuteAsync(new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken));
        }

        public async Task<int> ExecuteNonQueryAsync(NpgsqlCommand command, CancellationToken cancellationToken = default)
        {
            return await command.ExecuteNonQueryAsync(cancellationToken);
        }

        public async Task<NpgsqlConnection> GetNpgSqlConnection(CancellationToken cancellationToken = default)
        {
            var connection = GetConnection();
            await connection.OpenAsync(cancellationToken);
            return connection;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            IEnumerable<T> objs;
            await using NpgsqlConnection connection = GetConnection();
            objs = await connection.QueryAsync<T>(new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken));
            return objs;
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection connection = GetConnection();
            var obj = await connection.QueryFirstOrDefaultAsync<T>(new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken));
            return obj!;
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            await using NpgsqlConnection connection = GetConnection();
            var obj = await connection.QuerySingleOrDefaultAsync<T>(new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken));
            return obj!;
        }

        public async Task<IDbConnection> GetConnection(CancellationToken cancellationToken = default)
        {
            var connection = GetConnection();
            await connection.OpenAsync(cancellationToken);
            return connection;
        }
    }
}
