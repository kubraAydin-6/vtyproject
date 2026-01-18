using Dapper;
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
    public class CommentRepository : ICommentRepository
    {
        private readonly IFreKEDbHelper _dbHelper;

        public CommentRepository(IFreKEDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            var query = @"Select * from Comments where id=@id";
            var parameters = new { id };
            return await _dbHelper.QueryFirstOrDefaultAsync<Comment>(query, parameters);
        }
        public async Task<int> AddAsync(Comment comment)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"insert into comments (content, commentedbyid, commentedtargetid, createddate, updateddate)
            values (@content,@commentedbyid, @commentedtargetid, @createddate, @updateddate)";

            var parameters = new
            {
                comment.Content,
                comment.CommentedById,
                comment.CommentedTargetId,
                comment.CreatedDate,
                comment.UpdatedDate
            };

            await using var command = _dbHelper.CreateCommand(query, connection);
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("content", parameters.Content));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("commentedbyid", parameters.CommentedById));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("commentedtargetid", parameters.CommentedTargetId));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("createdDate", parameters.CreatedDate));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("updatedDate", parameters.UpdatedDate));
            await _dbHelper.ExecuteNonQueryAsync(command);
            await transaction.CommitAsync();
            await connection.CloseAsync();

            return 0;
        }

        public async Task<bool> UpdateAsync(Comment comment)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"Update comments
            set content=@content, commentedbyid=@commentedbyid, commentedtargetid=@commentedtargetid, updateddate=@updateddate where id=@id";

            if (comment.Id == null)
            {
                return false;
            }
            var parameters = new
            {
                comment.Id,
                comment.Content,
                comment.CommentedById,
                comment.CommentedTargetId,
                comment.UpdatedDate
            };

            await using var command = _dbHelper.CreateCommand(query, connection);

            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("id", parameters.Id));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("content", parameters.Content));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("commentedbyid", parameters.CommentedById));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("commentedtargetid", parameters.CommentedTargetId));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("updatedDate", parameters.UpdatedDate));
            await _dbHelper.ExecuteNonQueryAsync(command);
            await transaction.CommitAsync();
            await connection.CloseAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"Delete from comments where id=@id";

            var parameters = new
            {
                id,
            };
            if (id == null)
            {
                return false;
            }
            await using var command = _dbHelper.CreateCommand(query, connection);

            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("id", parameters.id));
            await _dbHelper.ExecuteNonQueryAsync(command);
            await transaction.CommitAsync();
            await connection.CloseAsync();

            return true;
        }

        
    }
}
