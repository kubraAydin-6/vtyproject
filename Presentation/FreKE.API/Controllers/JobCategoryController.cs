using FreKE.Application.Features.JobCategories.DTOs;
using FreKE.Application.Repositories;
using FreKE.Domain.Entities;
using FreKE.Persistence.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FreKE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCategoryController : ControllerBase
    {
        private readonly IJobCategoryRepository _jobCategoryRepository;

        public JobCategoryController(IJobCategoryRepository jobCategoryRepository)
        {
            _jobCategoryRepository = jobCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var job = await _jobCategoryRepository.GetByIdAsync(id);

            return Ok(job);
        }
        [HttpGet("List")]
        public async Task<IActionResult> GetAsync()
        {
            var jobCategories = await _jobCategoryRepository.GetAsync();
            return Ok(jobCategories);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateJobCategoryRequest request)
        {
            JobCategory jobCategory = new()
            {
                Name = request.Name,
            };
            await _jobCategoryRepository.AddAsync(jobCategory);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateJobCategoryRequest request)
        {
            JobCategory jobCategory = await _jobCategoryRepository.GetByIdAsync(request.Id);
            if (jobCategory == null)
                return NotFound();
            jobCategory.Id = request.Id;
            jobCategory.Name = request.Name;
            await _jobCategoryRepository.UpdateAsync(jobCategory);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            JobCategory jobCategory = await _jobCategoryRepository.GetByIdAsync(id);
            if (jobCategory == null)
                return NotFound();
            await _jobCategoryRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
