using FreKE.Application.Features.Comments.DTOs;
using FreKE.Application.Repositories;
using FreKE.Domain.Entities;
using FreKE.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FreKE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var comments = await _commentRepository.GetByIdAsync(id);

            return Ok(comments);
        }
        [HttpGet("ListCommentById")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var comments = await _commentRepository.GetAsync(id);
            return Ok(comments);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCommentRequest request)
        {
            Comment comment = new()
            {
                Content = request.Content,
                CommentedById = request.CommentedById,
                CommentedTargetId = request.CommentedTargetId
            };
            await _commentRepository.AddAsync(comment);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateCommentRequest request)
        {
            Comment comment = await _commentRepository.GetByIdAsync(request.Id);
            if (comment == null)
                return NotFound();
            comment.Id = request.Id;
            comment.Content = request.Content;
            await _commentRepository.UpdateAsync(comment);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Comment comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
                return NotFound();
            await _commentRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
