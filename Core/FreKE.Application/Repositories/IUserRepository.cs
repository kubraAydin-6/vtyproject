using FreKE.Application.Features.Users.DTOs;
using FreKE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<List<UserCommentDTO>> GetByIdCommentsAll(Guid id);
        Task<long> GetSumLikeAsync(Guid id);
        Task<long> GetSumCommentAsync(Guid id);
        Task<List<Job>> GetJobUserAsync(Guid id);
        Task<List<Job>> GetJobUserByAsync(Guid id);
        Task<int> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
    }
}
