using FreKE.Application.Features.Likes.DTOs;
using FreKE.Application.Repositories;
using FreKE.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FreKE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository _likeRepository;

        public LikeController(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var likes = await _likeRepository.GetByIdAsync(id);

            return Ok(likes);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateLikeRequest request)
        {
            Like like = new()
            {
                LikedById = request.LikedById,
                LikedUserId = request.LikedUserId
            };


            await _likeRepository.AddAsync(like);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Like like = await _likeRepository.GetByIdAsync(id);
            if (like == null)
                return NotFound();
            await _likeRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
