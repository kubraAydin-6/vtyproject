using FreKE.Application.Features.PriceOffers.DTOs;
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
        Task<List<GetPriceOfferDto>> GetAsync(Guid id);
        Task<List<GetGivenPriceOfferDto>> GetByIdProfileAsync(Guid id);
        Task<List<GetReceivedJobDto>> GetReceivedJobsAsync(Guid workerId);
        Task ApproveAsync(Guid offerId, Guid jobId);
        Task RejectOthersAsync(Guid offerId, Guid jobId);
        Task<int> AddAsync(PriceOffer priceOffer);
        Task<bool> UpdateAsync(PriceOffer priceOffer);
        Task<bool> DeleteAsync(Guid id);
    }
}
