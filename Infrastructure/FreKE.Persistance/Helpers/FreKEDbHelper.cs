using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreKE.Persistence.Dappers;
using Npgsql;

namespace FreKE.Persistence.Helpers
{
    public class FreKEDbHelper : NpgSqlHelper, IFreKEDbHelper
    {
        public FreKEDbHelper(string connectionString) : base(connectionString)
        {
        }
    }
}
