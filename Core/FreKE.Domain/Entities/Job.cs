using FreKE.Domain.Entities.Common;
using FreKE.Domain.Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Domain.Entities
{
    public class Job : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public DateTime? CompletedDate { get; set; }
        public JobStatus Status { get; set; }
        public Guid ApprovedOfferId { get; set; } //işi alan kişinin, fiyat teklifinin id si


        public Guid EmployerId { get; set; }
        public User Employer { get; set; }
        public Guid JobCategoryId { get; set; }
        public JobCategory JobCategory { get; set; }
        public ICollection<PriceOffer> PriceOffers { get; set; }
    }
}
