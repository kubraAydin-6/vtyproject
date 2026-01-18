using FreKE.Application.Features.PriceOffers.DTOs;
using FreKE.Application.Repositories;
using FreKE.Domain.Entities;
using FreKE.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FreKE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceOfferController : ControllerBase
    {
        private readonly IPriceOfferRepository _priceOfferRepository;
        public PriceOfferController(IPriceOfferRepository priceOfferRepository)
        {
            _priceOfferRepository = priceOfferRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var priceOffer = await _priceOfferRepository.GetByIdAsync(id);

            return Ok(priceOffer);
        }
        [HttpGet("{jobId}/PriceOfferList")]
        public async Task<IActionResult> GetAsync(Guid? id)
        {
            var priceOffers = await _priceOfferRepository.GetAsync(id.Value);
            return Ok(priceOffers);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatePriceOfferRequest request)
        {
            PriceOffer priceOffer = new()
            {
                OfferedPrice = request.OfferedPrice,
                WorkerId = request.WorkerId,
                JobId = request.JobId

            };
            await _priceOfferRepository.AddAsync(priceOffer);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdatePriceOfferRequest request)
        {
            PriceOffer priceOffer = await _priceOfferRepository.GetByIdAsync(request.Id);
            if (priceOffer == null)
                return NotFound();
            priceOffer.Id = request.Id;
            priceOffer.OfferedPrice = request.OfferedPrice;
            priceOffer.WorkerId = request.WorkerId;
            priceOffer.JobId = request.JobId;

            await _priceOfferRepository.UpdateAsync(priceOffer);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            PriceOffer priceOffer = await _priceOfferRepository.GetByIdAsync(id);
            if (priceOffer == null)
                return NotFound();
            await _priceOfferRepository.DeleteAsync(id);
            return Ok();
        }
        [HttpPut("Approve")]
        public async Task<IActionResult> ApproveAsync(ApprovePriceOfferRequest request)
        {
            await _priceOfferRepository.ApproveAsync(request.Id, request.JobId);
            return Ok();
        }
        [HttpPut("RejectOthers")]
        public async Task<IActionResult> RejectOthersAsync(RejectOthersPriceOfferRequest request)
        {
            await _priceOfferRepository.RejectOthersAsync(request.Id,request.JobId);
            return Ok();
        }
    }
}
