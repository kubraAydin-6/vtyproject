using FreKE.Domain.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Infrastructure.Providers
{
    public class UserProvider : IUserProvider
    {
        public string EncryptPassword(string password, string salt)
        {
            return EncryptText(password, salt);
        }

        private static string EncryptText(string text, string salt)
        {
            var sha = SHA512.Create();
            var reverse = string.Concat(salt.ToCharArray().OrderByDescending(x => x));
            var hashData = $"{reverse}_{text}";
            sha.ComputeHash(Encoding.UTF8.GetBytes(hashData));
            return BitConverter.ToString(sha.Hash!).Replace("-", string.Empty);
        }
    }
}
