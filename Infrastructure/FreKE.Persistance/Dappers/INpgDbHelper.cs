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
    public interface INpgDbHelper
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql,
            object? parameters = null,
            IDbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default);

        Task<T> QueryFirstOrDefaultAsync<T>(string sql,
            object? parameters = null,
            IDbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default);

        Task<T> QuerySingleOrDefaultAsync<T>(string sql,
            object? parameters = null,
            IDbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default);

        Task<int> ExecuteAsync(string sql,
            object? parameters = null,
            IDbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default);

        Task<IDbConnection> GetConnection(CancellationToken cancellationToken = default);

        Task<NpgsqlConnection> GetNpgSqlConnection(CancellationToken cancellationToken = default);

        Task<int> ExecuteNonQueryAsync(NpgsqlCommand command, CancellationToken cancellationToken = default);

        NpgsqlCommand CreateCommand(string sql, NpgsqlConnection connection);
    }
}
