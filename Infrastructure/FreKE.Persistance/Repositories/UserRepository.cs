using Dapper;
using FreKE.Application.Features.Users.DTOs;
using FreKE.Application.Repositories;
using FreKE.Domain.Entities;
using FreKE.Persistence.Helpers;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IFreKEDbHelper _dbHelper;

        public UserRepository(IFreKEDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();

            var query = @"Select * from users where id=@id";
            var parameters = new { id };

            return await _dbHelper.QueryFirstOrDefaultAsync<User>(query, parameters);

        }
        public async Task<int> AddAsync(User user)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"insert into users (name, surname, email, phone, createddate, updateddate)
            values (@name,@surname, @email, @phone, @createddate, @updateddate)";

            var parameters = new
            {
                user.Name,
                user.Surname,
                user.Email,
                user.Phone,
                user.CreatedDate,
                user.UpdatedDate
            };

            await using var command = _dbHelper.CreateCommand(query, connection);
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("name", parameters.Name));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("surname", parameters.Surname));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("email", parameters.Email));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("phone", parameters.Phone));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("createdDate", parameters.CreatedDate));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("updatedDate", parameters.UpdatedDate));
            await _dbHelper.ExecuteNonQueryAsync(command);
            await transaction.CommitAsync();
            await connection.CloseAsync();

            return 0;
        }
        public async Task<bool> UpdateAsync(User user)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"Update users
            set name=@name, surname=@surname, email=@email, phone=@phone , updateddate=@updateddate where id=@id";

            if (user.Id == null)
            {
                return false;
            }
            var parameters = new
            {
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.Phone,
                user.UpdatedDate,
            };

            await using var command = _dbHelper.CreateCommand(query, connection);

            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("id", parameters.Id));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("name", parameters.Name));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("surname", parameters.Surname));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("email", parameters.Email));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("phone", parameters.Phone));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("updateddate", parameters.UpdatedDate));

            await _dbHelper.ExecuteNonQueryAsync(command);
            await transaction.CommitAsync();
            await connection.CloseAsync();
            

            return true;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"Delete from users where id=@id";
            
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

        public async Task<long> GetSumLikeAsync(Guid id)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();

            var query = @"SELECT COUNT(*) FROM Likes WHERE LikedById = @id";
            var parameters = new { id };
            var result = await connection.ExecuteScalarAsync<long>(query, parameters);

            return result;
        }

        public async Task<long> GetSumCommentAsync(Guid id)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();

            var query = @"Select count(*) from Comments where CommentedById = @id";
            var parameters = new { id };
            var result = await connection.ExecuteScalarAsync<long>(query, parameters);

            return result;
        }

        public async Task<List<Job>> GetJobUserAsync(Guid id)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();

            var query = @"SELECT * FROM Jobs WHERE EmployerId = @id";

            var parameters = new { id };

            var result = await connection.QueryAsync<Job>(query, parameters);

            return result.ToList();
        }
        public async Task<List<Job>> GetJobUserByAsync(Guid id)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            var query = @"SELECT * FROM priceoffers WHERE workerid=@workerid";
            var parameters = new 
            { 
                WorkerId = id,
            };
            var result = await connection.QueryAsync<Job>(query, parameters);
            return result.ToList();
        }

        public async Task<List<UserCommentDTO>> GetByIdCommentsAll(Guid id)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();

            var query = @"select
                ub.id as UserId,
                ub.name as UserName,
                ub.surname as UserSurname,

                c.id as CommentId,
                c.content as Content,
                
                
                ut.id as CommentUserId,
                ut.name as CommentUserName,
                ut.surname as CommentUserSurname

                from users ub left join Comments c
                                ON ub.id = c.commentedbyid
                            left join users ut
                                ON ut.id = c.commentedtargetid
                where ub.id=@id";

            var parameters = new { id = id };
            var result = await _dbHelper.QueryAsync<UserCommentDTO>(query, parameters);

            return result.ToList();
        }
    }
}