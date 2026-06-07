using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.PriceOffers.DTOs
{
    public class GetGivenPriceOfferDto
    {
            public Guid Id { get; set; }

            public decimal OfferedPrice { get; set; }

            public Guid JobId { get; set; }

            public string JobTitle { get; set; }

            public string Status { get; set; }
    }
}
