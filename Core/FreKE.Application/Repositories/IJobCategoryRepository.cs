using FreKE.Application.Features.JobCategories.DTOs;
using FreKE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Repositories
{
    public interface IJobCategoryRepository
    {
        Task<JobCategory> GetByIdAsync(Guid id);
        Task<List<JobCategory>> GetAsync();
        Task<List<GetJobCategoryTotalDTO>> JobCategoryTotalAsync();
        Task<int> AddAsync(JobCategory jobcategory);
        Task<bool> UpdateAsync(JobCategory jobcategory);
        Task<bool> DeleteAsync(Guid id);
    }
}
