using FreKE.Application.Features.Users.DTOs;
using FreKE.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace FreKE.Application.Security.JWT
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly TokenOptions _tokenOptions;
        public TokenGenerator(
            IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }

        public async Task<TokenResponse> GenerateJwtAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey!));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

            var securityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: GenerateClaims(user));

            var tokenHandler = new JwtSecurityTokenHandler();
            await Task.CompletedTask;
            return new TokenResponse(tokenHandler.WriteToken(securityToken),
                expiration);
        }
        private static List<Claim> GenerateClaims(User user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)!));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Surname, user.Surname));

            return claims;
        }
    }
}
