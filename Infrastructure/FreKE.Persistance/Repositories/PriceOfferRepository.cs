using Dapper;
using FreKE.Application.Repositories;
using FreKE.Domain.Entities;
using FreKE.Domain.Entities.enums;
using FreKE.Persistence.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Persistence.Repositories
{
    public class PriceOfferRepository : IPriceOfferRepository
    {
        private readonly IFreKEDbHelper _dbHelper;

        public PriceOfferRepository(IFreKEDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<PriceOffer> GetByIdAsync(Guid id)
        {
            var query = @"Select * from PriceOffers where id=@id";
            var parameters = new { id };
            return await _dbHelper.QueryFirstOrDefaultAsync<PriceOffer>(query, parameters);
        }
        public async Task<List<PriceOffer>> GetAsync(Guid id)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            var query = "Select * from priceoffers where jobid = @id";
            var parameters = new 
            { 
                id 
            };

            var result = await connection.QueryAsync<PriceOffer>(query, parameters);

            return result.ToList();

        }
        public async Task<int> AddAsync(PriceOffer priceOffer)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"insert into priceoffers (offeredprice, workerid, jobid, createddate, updateddate, status)
            values (@offeredprice,@workerid, @jobid, @createddate, @updateddate, @status)";

            var parameters = new
            {
                priceOffer.OfferedPrice,
                priceOffer.WorkerId,
                priceOffer.JobId,
                priceOffer.CreatedDate,
                priceOffer.UpdatedDate,
                priceOffer.priceOfferStatus
            };

            await using var command = _dbHelper.CreateCommand(query, connection);
            command.Parameters.Add(new Npgsql.NpgsqlParameter<decimal>("offeredprice", parameters.OfferedPrice));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("workerid", parameters.WorkerId));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("jobid", parameters.JobId));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("createdDate", parameters.CreatedDate));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("updatedDate", parameters.UpdatedDate));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<short>("status", (short)parameters.priceOfferStatus));
            await _dbHelper.ExecuteNonQueryAsync(command);
            await transaction.CommitAsync();
            await connection.CloseAsync();

            return 0;
        }

        public async Task<bool> UpdateAsync(PriceOffer priceOffer)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"Update priceoffers
            set offeredprice=@offeredprice, workerid=@workerid, jobid=@jobid, updateddate=@updateddate, status=@status where id=@id";

            if (priceOffer.Id == null)
            {
                return false;
            }
            var parameters = new
            {
                priceOffer.Id,
                priceOffer.OfferedPrice,
                priceOffer.WorkerId,
                priceOffer.JobId,
                priceOffer.UpdatedDate,
                priceOffer.priceOfferStatus
            };

            await using var command = _dbHelper.CreateCommand(query, connection);

            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("id", parameters.Id));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<decimal>("offeredprice", parameters.OfferedPrice));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("workerid", parameters.WorkerId));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("jobid", parameters.JobId));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<DateTime>("updateddate", parameters.UpdatedDate));
            command.Parameters.Add(new Npgsql.NpgsqlParameter<short>("status", (short)parameters.priceOfferStatus));

            await _dbHelper.ExecuteNonQueryAsync(command);
            await transaction.CommitAsync();
            await connection.CloseAsync();


            return true;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            var query = @"Delete from priceoffer where id=@id";

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

        public async Task ApproveAsync(Guid offerId, Guid jobId)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            try
            {   
                var query = @"Update jobs set status=@status, approvedofferid=@approvedofferid where id=@id;
                            Update priceoffers set status=@offerstatus where id=@approvedofferid";

                var parameters = new
                {
                    Status = (short)JobStatus.InProcess,
                    ApprovedOfferId = offerId,
                    Id = jobId,
                    PriceOfferStatus = (short)PriceOfferStatus.Approved

                };
                await using var command = _dbHelper.CreateCommand(query, connection);
                command.Parameters.Add(new Npgsql.NpgsqlParameter<short>("status", parameters.Status));
                command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("approvedofferid", parameters.ApprovedOfferId));
                command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("id", parameters.Id));
                command.Parameters.Add(new Npgsql.NpgsqlParameter<short>("offerstatus", parameters.PriceOfferStatus));
                await _dbHelper.ExecuteNonQueryAsync(command);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    await connection.CloseAsync();
            }

        }
        public async Task RejectOthersAsync(Guid offerId,Guid jobId)
        {
            await using var connection = await _dbHelper.GetNpgSqlConnection();
            await using var transaction = await connection.BeginTransactionAsync();
            try
            {
                var query = @"Update priceoffers set status=@offerstatus where jobid=@jobid and id=@id";

                await using var command = _dbHelper.CreateCommand(query, connection);

                var parameters = new
                {
                    Id = offerId,
                    JobId = jobId,
                    PriceOfferStatus = (short)PriceOfferStatus.Rejected
                };
                command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("id", parameters.Id));
                command.Parameters.Add(new Npgsql.NpgsqlParameter<Guid>("jobid", parameters.JobId));
                command.Parameters.Add(new Npgsql.NpgsqlParameter<short>("offerstatus", parameters.PriceOfferStatus));
                await _dbHelper.ExecuteNonQueryAsync(command);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    await connection.CloseAsync();
            }
        }
    }
}
