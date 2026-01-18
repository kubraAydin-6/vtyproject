using Dapper;
using FreKE.Application.Features.JobCategories.DTOs;
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
    public class JobCategoryRepository : IJobCategoryRepository
    {
        private readonly IFreKEDbHelper _dbHelper;

        public JobCategoryRepository(IFreKEDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<JobCategory> GetByIdAsync(Guid id)
        {
            var query = @"Select * from JobCategories where id=@id";
            var parameters = new { id };
            return await _dbHelper.QueryFirstOrDefaultAsync<JobCategory>(query, parameters);
        }
        public async Task<int> AddAsync(JobCategory jobcategory)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"insert into jobcategories (name, createddate, updateddate)
            values (@name, @createddate, @updateddate)";

            var parameters = new
            {
                jobcategory.Name,
                jobcategory.CreatedDate,
                jobcategory.UpdatedDate
            };

            await using var command = _dbHelper.CreateCommand(query, connection);
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("name", parameters.Name));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("createdDate", parameters.CreatedDate));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("updatedDate", parameters.UpdatedDate));
            await _dbHelper.ExecuteNonQueryAsync(command);
            await transaction.CommitAsync();
            await connection.CloseAsync();

            return 0;
        }
        public async Task<bool> UpdateAsync(JobCategory jobcategory)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"Update jobcategories
            set name=@name, updateddate=@updateddate where id=@id";

            if (jobcategory.Id == null)
            {
                return false;
            }
            var parameters = new
            {
                jobcategory.Id,
                jobcategory.Name,
                jobcategory.UpdatedDate
            };

            await using var command = _dbHelper.CreateCommand(query, connection);

            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("id", parameters.Id));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<string>("name", parameters.Name));
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
            var query = @"Delete from jobcategories where id=@id";

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

        public async Task<List<JobCategory>> GetAsync()
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            var query = "Select * from jobcategories";
            var result = await connection.QueryAsync<JobCategory>(query);
            return result.ToList();
        }

        public async Task<List<GetJobCategoryTotalDTO>> JobCategoryTotalAsync()
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            var query = @"select
                jc.Id AS Id,
                jc.Name AS Name,      
                COUNT(j.Id) AS TotalJobs
                FROM JobCategories jc           
                LEFT JOIN Jobs j                
                ON j.JobCategoryId = jc.Id
                GROUP BY jc.Id,jc.Name";

            var result = await connection.QueryAsync<GetJobCategoryTotalDTO>(query);
            return result.ToList();
        }
    }
}
