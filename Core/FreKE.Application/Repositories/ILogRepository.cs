using FreKE.Application.Features.Logs;
using FreKE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Repositories
{
    public interface ILogRepository
    {
        Task<int> AddAsync(Log log);
        Task<List<GetLogTotalDto>> GetLogTotalAsync();
    }
}
