using FreKE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Repositories
{
    public interface ILikeRepository
    {
        Task<Like> GetByIdAsync(Guid id);
        Task<List<Like>> GetAsync();
        Task<int> AddAsync(Like like);
        Task<bool> DeleteAsync(Guid id);
    }
}
