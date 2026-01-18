using FreKE.Application.Features.Users.DTOs;
using FreKE.Application.Repositories;
using FreKE.Application.Security.JWT;
using FreKE.Domain.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreKE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProvider _userProvider;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(IUserRepository userRepository, IUserProvider userProvider, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _userProvider = userProvider;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user is null)
            {
                return NotFound();
            }

            var encryptedPass = _userProvider.EncryptPassword(request.Password, request.Email);

            if (!string.Equals(user.Password, encryptedPass)) 
            {
                return BadRequest("Kullanıcı Adı veya Şifre Hatalı !");
            }

            var token = await _tokenGenerator.GenerateJwtAccessToken(user);

            return Ok(token);
        }
    }
}
