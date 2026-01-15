using FreKE.Domain.Entities.Common;
using FreKE.Domain.Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Domain.Entities
{
    public class PriceOffer : BaseEntity
    {
        public decimal OfferedPrice { get; set; }
        public PriceOfferStatus priceOfferStatus{ get; set; }

        public Guid WorkerId { get; set; }
        public User Worker { get; set; }
        public Guid JobId { get; set; }
        public Job Job { get; set; }
    }
}
