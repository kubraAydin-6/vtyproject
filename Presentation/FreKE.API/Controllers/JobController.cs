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

        [HttpGet("JobInformation")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var job = await _jobRepository.GetByIdAsync(id);

            return Ok(job);
        }
        [HttpGet("{CategoryId}/JobList")]
        public async Task<IActionResult> GetAsync(Guid? id)
        {
            var jobs = await _jobRepository.GetAsync(id);
            return Ok(jobs);
        }
        [HttpPost("JobInformation")]
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
        [HttpPut("JobInformation")]
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
        [HttpPut("JobStatusInformation")]
        public async Task<IActionResult> CompletedAsync(Guid id, Guid employerid)
        {
            Job job = await _jobRepository.GetByIdAsync(id);
            if (job.EmployerId == employerid)
                await _jobRepository.CompletedAsync(id);
            return Ok();
            return BadRequest();
        }
        [HttpGet("TotalPriceOfferJob")]
        public async Task<IActionResult> GetJobsPriceOfferTotalsAsync()
        {
            var totalPrice = await _jobRepository.GetJobsPriceOfferTotalsAsync();
            return Ok(totalPrice);
        }
        [HttpGet("JobDateTotal")]
        public async Task<IActionResult> GetJobDateTotalsAsync()
        {
            var jobDateTotals = await _jobRepository.GetJobDateTotalsAsync();
            return Ok(jobDateTotals);
        }
        [HttpGet("JobOfTheWeek")]
        public async Task<IActionResult> GetJobWeekAsync()
        {
            var jobWeek = await _jobRepository.GetJobWeekAsync();
            return Ok(jobWeek);
        }
        [HttpGet("JobOfTheMonth")]
        public async Task<IActionResult> GetJobMonthAsync()
        {
            var jobMonth = await _jobRepository.GetJobMonthAsync();
            return Ok(jobMonth);
        }
        [HttpGet("JobOfTheDay")]
        public async Task<IActionResult> GetJobDayAsync()
        {
            var jobDay = await _jobRepository.GetJobDayAsync();
            return Ok(jobDay);
        }
        [HttpGet("JobCategoryOfTheWeek")]
        public async Task<IActionResult> GetJobCategoryWeekAsync(Guid id)
        {
            var jobCategoryWeek = await _jobRepository.GetJobCategoryWeekAsync(id);
            return Ok(jobCategoryWeek);
        }
    }
}
