using FreKE.Application.Features.Jobs.DTOs;
using FreKE.Application.Features.Likes.DTOs;
using FreKE.Application.Features.PriceOffers.DTOs;
using FreKE.Application.Repositories;
using FreKE.Domain.Entities;
using FreKE.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FreKE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MeController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPriceOfferRepository _priceOfferRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ILikeRepository _likeRepository;

        public MeController(IUserRepository userRepository, IHttpContextAccessor httpContextAcessor, ICommentRepository commentRepository, IPriceOfferRepository priceOfferRepository, IJobRepository jobRepository, ILikeRepository likeRepository)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAcessor;
            _priceOfferRepository = priceOfferRepository;
            _jobRepository = jobRepository;
            _likeRepository = likeRepository;
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetByIdAsync()
        {
            var userId = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier); ////JWT token içindeki kullanıcı ID’sini buluyoruz.

            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId.Value));
            return Ok(user);
        }

        [HttpGet("CommentProfile")]
        public async Task<IActionResult> GetByIdCommentsAll()
        {
            var userId = _httpContextAccessor.HttpContext!
            .User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var users = await _userRepository.GetByIdCommentsAll(Guid.Parse(userId.Value));
            return Ok(users);
        }

        [HttpGet("GivenJobProfile")]
        public async Task<IActionResult> GetJobUserAsync()
        {
            var userId = _httpContextAccessor.HttpContext!
            .User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var users = await _userRepository.GetJobUserAsync(Guid.Parse(userId.Value));
            return Ok(users);
        }

        [HttpGet("TotalLikesProfile")]
        public async Task<IActionResult> GetSumLikeAsync()
        {
            var userId = _httpContextAccessor.HttpContext!
            .User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var users = await _userRepository.GetSumLikeAsync(Guid.Parse(userId.Value));
            return Ok(users);
        }

        [HttpPost("CreatePriceOfferProfile")]
        public async Task<IActionResult> AddAsync(CreatePriceOfferRequest request)
        {
            var userId = _httpContextAccessor.HttpContext!
            .User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            PriceOffer priceOffer = new()
            {
                OfferedPrice = request.OfferedPrice,
                WorkerId = Guid.Parse(userId.Value),
                JobId = request.JobId

            };
            await _priceOfferRepository.AddAsync(priceOffer);
            return Ok();
        }
        [HttpPost("CreateJobProfile")]
        public async Task<IActionResult> CreateAsync(CreateJobRequest request)
        {
            var userId = _httpContextAccessor.HttpContext!
            .User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            Job job = new()
            {
                Title = request.Title,
                Description = request.Description,
                Budget = request.Budget,
                CompletedDate = request.CompletedDate,
                Status = request.Status,
                EmployerId = Guid.Parse(userId.Value),
                JobCategoryId = request.JobCategoryId
            };
            await _jobRepository.AddAsync(job);
            return Ok();
        }

        [HttpDelete("DeleteJobProfile/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Job job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
                return NotFound();
            await _jobRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("UpdateJobProfile")]
        public async Task<IActionResult> UpdateAsync(UpdateJobRequest request)
        {
            var userId = _httpContextAccessor.HttpContext!
            .User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            Job job = await _jobRepository.GetByIdAsync(request.Id);
            if (job == null)
                return NotFound();
            job.Id = request.Id;
            job.Title = request.Title;
            job.Description = request.Description;
            job.Budget = request.Budget;
            job.CompletedDate = request.CompletedDate;
            job.Status = request.Status;
            job.EmployerId = Guid.Parse(userId.Value);
            job.JobCategoryId = request.JobCategoryId;

            await _jobRepository.UpdateAsync(job);
            return Ok();
        }
        [HttpGet("PriceOfferProfile/{id}")]
        public async Task<IActionResult> GetAsync(Guid? id)
        {
            var priceOffers = await _priceOfferRepository.GetAsync(id.Value);
            return Ok(priceOffers);
        }
        [HttpGet("GivenPriceOfferProfile")]
        public async Task<IActionResult> GetByIdProfileAsync()
        {
            var userId = _httpContextAccessor.HttpContext!
                .User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var workerId = Guid.Parse(userId.Value);

            var priceOffers =
                await _priceOfferRepository.GetByIdProfileAsync(workerId);

            return Ok(priceOffers);
        }

        [HttpGet("ReceivedJobsProfile")]
        public async Task<IActionResult> GetReceivedJobsAsync()
        {
            var userId = _httpContextAccessor.HttpContext!
                .User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var workerId = Guid.Parse(userId.Value);

            var jobs = await _priceOfferRepository.GetReceivedJobsAsync(workerId);

            return Ok(jobs);
        }
        [HttpPut("ApproveProfile")]
        public async Task<IActionResult> ApproveAsync(ApprovePriceOfferRequest request)
        {
            await _priceOfferRepository.ApproveAsync(request.OfferId, request.JobId);
            return Ok();
        }
        [HttpPut("CompleteJobProfile/{jobId}")]
        public async Task<IActionResult> Complete(Guid jobId)
        {
            var userId = Guid.Parse(
            _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _jobRepository.CompletedAsync(jobId, userId);

            if (!result)
                return Forbid("Bu işi tamamlamaya yetkin yok.");

            return Ok();
        }

        [HttpPost("CreateLikeProfile")]
        public async Task<IActionResult> CreateAsync(CreateLikeRequest request)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!
                .User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var likedById = Guid.Parse(userIdClaim);

            Like like = new()
            {
                LikedById = likedById,
                LikedUserId = request.LikedUserId
            };

            await _likeRepository.AddAsync(like);
            return Ok();
        }

        [HttpGet("budgetAsc")]
        public async Task<IActionResult> GetBudgetAsc()
        {
            var result = await _jobRepository.GetJobsByBudgetAscAsync();
            return Ok(result);
        }

        [HttpGet("budgetDesc")]
        public async Task<IActionResult> GetBudgetDesc()
        {
            var result = await _jobRepository.GetJobsByBudgetDescAsync();
            return Ok(result);
        }
    }
}
//_httpContextAcessor: Şu an giriş yapan kullanıcının bilgilerine erişmek için kullanılıyor. 
