using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Domain.Providers
{
    public interface IUserProvider : IEncryptionProvider
    {
    }

    public interface IEncryptionProvider
    {
        string EncryptPassword(string password, string salt);
    }
}
