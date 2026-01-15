using FreKE.Application.Features.Users.DTOs;
using FreKE.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Security.JWT
{
    public interface ITokenGenerator
    {
        Task<TokenResponse> GenerateJwtAccessToken(User user);
    }
}
