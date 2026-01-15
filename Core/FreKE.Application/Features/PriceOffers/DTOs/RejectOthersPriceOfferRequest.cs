using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.PriceOffers.DTOs
{
    public class RejectOthersPriceOfferRequest
    {
        public Guid JobId { get; set; }
        public Guid Id { get; set; }
    }
}
