using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Jobs.DTOs
{
    public class GetJobsPriceOfferTotalDTO
    {
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public decimal PriceOfferTotal { get; set; }
    }
}