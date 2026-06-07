using FreKE.Domain.Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.PriceOffers.DTOs
{
    public class GetReceivedJobDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public JobStatus Status { get; set; }
        public Guid WorkerId { get; set; }
    }
}
