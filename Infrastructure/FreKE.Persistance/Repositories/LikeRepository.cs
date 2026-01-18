using Dapper;
using FreKE.Application.Repositories;
using FreKE.Domain.Entities;
using FreKE.Persistence.Contexts;
using FreKE.Persistence.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Persistence.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly IFreKEDbHelper _dbHelper;

        public LikeRepository(IFreKEDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<Like> GetByIdAsync(Guid id)
        {
            var query = @"Select * from Likes where id=@id";
            var parameters = new { id };
            return await _dbHelper.QueryFirstOrDefaultAsync<Like>(query, parameters);
        }
        public async Task<int> AddAsync(Like like)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"insert into likes (likedbyid, likeduserid, createddate, updateddate)
            values (@likedbyid,@likeduserid, @createddate, @updateddate)";

            var parameters = new
            {
                like.LikedById,
                like.LikedUserId,
                like.CreatedDate,
                like.UpdatedDate
            };

            await using var command = _dbHelper.CreateCommand(query, connection);
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("likedbyid", parameters.LikedById));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("likeduserid", parameters.LikedUserId));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("createdDate", parameters.CreatedDate));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("updatedDate", parameters.UpdatedDate));
            await _dbHelper.ExecuteNonQueryAsync(command);
            await transaction.CommitAsync();
            await connection.CloseAsync();

            return 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"Delete from likes where id=@id";

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
