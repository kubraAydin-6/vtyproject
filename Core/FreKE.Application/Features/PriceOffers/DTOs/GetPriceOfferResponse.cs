using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.PriceOffers.DTOs
{
    public class GetPriceOfferResponse
    {
        public Guid Id { get; set; }
        public Guid EmployerId { get; set; }
        public string EmployerName { get; set; }
        public string EmployerSurname { get; set; }
        public Guid WorkerId { get; set; }
        public string WorkerName { get; set; }
        public string WorkerSurname { get; set; }
        public decimal OfferedPrice { get; set; }
        public Guid JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Status { get; set; }
        public Guid JobCategoryId { get; set; }
        public string JobCategoryName { get; set; }
    }
}
