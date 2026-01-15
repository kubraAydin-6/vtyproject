using FreKE.Application.Repositories;
using FreKE.Domain.Entities;
using FreKE.Persistence.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Persistence.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IFreKEDbHelper _dbHelper;

        public LogRepository(IFreKEDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<int> AddAsync(Log log)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"insert into logs (loglevel, endpoint, userid, request, actionmethod, processtime)
            values (@loglevel,@endpoint, @userid, @request, @actionmethod, @processtime)";

            var parameters = new
            {
                log.LogLevel,
                log.Endpoint,
                log.UserId,
                log.Request,
                log.ActionMethod,
                log.ProcessTime
            };

            await using var command = _dbHelper.CreateCommand(query, connection);
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("logLevel", parameters.LogLevel));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("endpoint", parameters.Endpoint));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid?>("userid", parameters.UserId));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("request", parameters.Request));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("actionMethod", parameters.ActionMethod));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("processTime", parameters.ProcessTime));
            await _dbHelper.ExecuteNonQueryAsync(command);
            await transaction.CommitAsync();
            await connection.CloseAsync();

            return 0;
        }
    }
}
