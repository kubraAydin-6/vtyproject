using FreKE.Application.Features.Jobs.DTOs;
using FreKE.Application.Repositories;
using FreKE.Domain.Entities;
using FreKE.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FreKE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;

        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var job = await _jobRepository.GetByIdAsync(id);

            return Ok(job);
        }
        [HttpGet("List/CategoryId")]
        public async Task<IActionResult> GetAsync(Guid? id)
        {
            var jobs = await _jobRepository.GetAsync(id);
            return Ok(jobs);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateJobRequest request)
        {
            Job job = new()
            {
                Title = request.Title,
                Description = request.Description,
                Budget = request.Budget,
                CompletedDate = request.CompletedDate,
                Status = request.Status,
                EmployerId = request.EmployerId,
                JobCategoryId = request.JobCategoryId
            };
            await _jobRepository.AddAsync(job);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateJobRequest request)
        {
            Job job = await _jobRepository.GetByIdAsync(request.Id);
            if (job == null)
                return NotFound();
            job.Id = request.Id;
            job.Title = request.Title;
            job.Description = request.Description;
            job.Budget = request.Budget;
            job.CompletedDate = request.CompletedDate;
            job.Status = request.Status;
            job.EmployerId = request.EmployerId;
            job.JobCategoryId = request.JobCategoryId;

            await _jobRepository.UpdateAsync(job);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Job job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
                return NotFound();
            await _jobRepository.DeleteAsync(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> CompletedAsync(Guid id, Guid employerid)
        {
            Job job = await _jobRepository.GetByIdAsync(id);
            if (job.EmployerId == employerid)
                await _jobRepository.CompletedAsync(id);
                return Ok();
            return BadRequest();
            

        }
    }
}
