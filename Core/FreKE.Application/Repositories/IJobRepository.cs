using FreKE.Application.Features.JobCategories.DTOs;
using FreKE.Application.Features.Jobs.DTOs;
using FreKE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Repositories
{
    public interface IJobRepository
    {
        Task<Job> GetByIdAsync(Guid id);
        Task<List<Job>> GetAsync(Guid? id);
        Task<List<GetJobsPriceOfferTotalDTO>> GetJobsPriceOfferTotalsAsync();
        Task<List<GetJobDateTotalDto>> GetJobDateTotalsAsync();
        Task<List<Job>> GetJobDayAsync();
        Task<List<Job>> GetJobWeekAsync();
        Task<List<Job>> GetJobMonthAsync();
        Task<List<Job>> GetJobCategoryWeekAsync(Guid id);
        Task<int> AddAsync(Job job);
        Task<bool> UpdateAsync(Job job);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> CompletedAsync(Guid jobid);
    }
}
