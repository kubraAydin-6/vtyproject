using FreKE.Application.Features.Users.DTOs;
using FreKE.Application.Repositories;
using FreKE.Domain.Entities;
using FreKE.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FreKE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return Ok(user);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserRequest request)
        {
            User user = new()
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Phone = request.Phone
            };
            await _userRepository.AddAsync(user);
            
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                return NotFound();
            user.Id = request.Id;
            user.Name = request.Name;
            user.Surname = request.Surname;
            user.Email = request.Email;
            user.Phone = request.Phone;

            await _userRepository.UpdateAsync(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            User user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            await _userRepository.DeleteAsync(id);
            return Ok();

        }

        [HttpGet("SumLike")]
        public async Task<IActionResult> GetSumLikeAsync(Guid id)
        {
            var users = await _userRepository.GetSumLikeAsync(id);
            return Ok(users);
        }

        [HttpGet("SumComment")]
        public async Task<IActionResult> GetSumCommentAsync(Guid id)
        {
            var users = await _userRepository.GetSumCommentAsync(id);
            return Ok(users);
        }

        [HttpGet("{id}/jobs")]
        public async Task<IActionResult> GetJobUserAsync(Guid id)
        {
            var users = await _userRepository.GetJobUserAsync(id);
            return Ok(users);
        }

        [HttpGet("{id}/jobsby")]
        public async Task<IActionResult> GetJobUserByAsync(Guid id)
        {
            var users = await _userRepository.GetJobUserByAsync(id);
            return Ok(users);
        }

        [HttpGet("{id}/commentsAll")]
        public async Task<IActionResult> GetByIdCommentsAll(Guid id)
        {
            var users = await _userRepository.GetByIdCommentsAll(id);
            return Ok(users);
        }

    }
}


