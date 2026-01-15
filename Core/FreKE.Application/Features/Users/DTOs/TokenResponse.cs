using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Users.DTOs
{
    public class TokenResponse
    {
        public TokenResponse(string token,
            DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
