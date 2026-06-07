using FreKE.Domain.Entities;
using FreKE.Domain.Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.PriceOffers.DTOs
{
    public class GetPriceOfferDto
    {
        public Guid Id { get; set; }
        public decimal OfferedPrice { get; set; }
        public PriceOfferStatus priceOfferStatus { get; set; }

        public Guid WorkerId { get; set; }
        public string WorkerName { get; set; }
        public string WorkerSurname { get; set; }
        public User Worker { get; set; }
        public Guid JobId { get; set; }
        public Job Job { get; set; }
    }
}
