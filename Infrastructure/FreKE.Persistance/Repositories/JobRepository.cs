using Dapper;
using FreKE.Application.Repositories;
using FreKE.Domain.Entities;
using FreKE.Domain.Entities.enums;
using FreKE.Persistence.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Persistence.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IFreKEDbHelper _dbHelper;

        public JobRepository(IFreKEDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<Job> GetByIdAsync(Guid id)
        {
            var query = @"Select * from Jobs where id=@id";
            var parameters = new { id };
            return await _dbHelper.QueryFirstOrDefaultAsync<Job>(query, parameters);
        }
        public async Task<int> AddAsync(Job job)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"insert into jobs (title, description, budget, completeddate, status, employerid, jobcategoryid, createddate, updateddate, approvedofferid)
            values (@title,@description, @budget, @completeddate, @status , @employerid, @jobcategoryid, @createddate, @updateddate, @approvedofferid)";

            var parameters = new
            {
                job.Title,
                job.Description,
                job.Budget,
                job.CompletedDate,
                job.Status,
                job.EmployerId,
                job.JobCategoryId,
                job.CreatedDate,
                job.UpdatedDate,
                job.ApprovedOfferId
            };

            await using var command = _dbHelper.CreateCommand(query, connection);
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("title", parameters.Title));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("description", parameters.Description));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<decimal>("budget", parameters.Budget));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime?>("completeddate", parameters.CompletedDate));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<short>("status", (short)parameters.Status));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("employerid", parameters.EmployerId));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("jobcategoryid", parameters.JobCategoryId));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("createdDate", parameters.CreatedDate));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("updatedDate", parameters.UpdatedDate));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("approvedofferid", parameters.ApprovedOfferId));
            await _dbHelper.ExecuteNonQueryAsync(command);
            await transaction.CommitAsync();
            await connection.CloseAsync();

            return 0;
        }

        public async Task<bool> UpdateAsync(Job job)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"Update jobs                                   
            set title=@title, description=@description, budget=@budget, completeddate=@completeddate , status=@status, employerid=@employerid, jobcategoryid=@jobcategoryid, updateddate=@updateddate, approvedofferid=@approvedofferid  where id=@id";

            if (job.Id == null)
            {
                return false;
            }
            var parameters = new
            {
                job.Id,
                job.Title,
                job.Description,
                job.Budget,
                job.CompletedDate,
                job.Status,
                job.EmployerId,
                job.JobCategoryId,
                job.UpdatedDate,
                job.ApprovedOfferId
            };

            await using var command = _dbHelper.CreateCommand(query, connection);

            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("id", parameters.Id));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("title", parameters.Title));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("description", parameters.Description));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<decimal>("budget", parameters.Budget));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime?>("completeddate", parameters.CompletedDate));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<short>("status", (short)parameters.Status));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("employerid", parameters.EmployerId));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("jobcategoryid", parameters.JobCategoryId));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("updatedDate", parameters.UpdatedDate));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("approvedofferid", parameters.ApprovedOfferId));
            await _dbHelper.ExecuteNonQueryAsync(command);
            await transaction.CommitAsync();
            await connection.CloseAsync();

            return true;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"Delete from jobs where id=@id";

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
            public async Task<List<Job>> GetAsync(Guid? id)
            {
            await using var connection = await _dbHelper.GetNpgSqlConnection();

            var query = @"SELECT * FROM jobs";

            object parameters = null;

            if (id.HasValue)
            {
                query += " WHERE jobcategoryid = @CategoryId";
                parameters = new { CategoryId = id };
            }

            var result = await connection.QueryAsync<Job>(query, parameters);

            return result.ToList();
            }

        public async Task<bool> CompletedAsync(Guid jobid)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            var query = @"Update jobs 
                        set status=@status where id = @id";
            var parameters = new {
                Status = JobStatus.Completed,
                Id = jobid,
            };
            await using var command = _dbHelper.CreateCommand(query, connection);
            command.Parameters.Add(new Npgsql.NpgsqlParameter<short>("status", (short)parameters.Status));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("id", parameters.Id));
            await _dbHelper.ExecuteNonQueryAsync(command);
            await connection.CloseAsync();
            return true;
        }
    }
}
