using FreKE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Repositories
{
    public interface IPriceOfferRepository
    {
        Task<PriceOffer> GetByIdAsync(Guid id);
        Task<List<PriceOffer>> GetAsync(Guid id);
        Task ApproveAsync(Guid offerId, Guid jobId);
        Task RejectOthersAsync(Guid offerId, Guid jobId);
        Task<int> AddAsync(PriceOffer priceOffer);
        Task<bool> UpdateAsync(PriceOffer priceOffer);
        Task<bool> DeleteAsync(Guid id);
    }
}
