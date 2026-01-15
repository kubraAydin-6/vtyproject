using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.PriceOffers.DTOs
{
    public class ApprovePriceOfferRequest
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }

    }
}
