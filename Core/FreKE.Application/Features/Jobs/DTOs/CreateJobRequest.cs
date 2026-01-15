using FreKE.Domain.Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Jobs.DTOs
{
    public class CreateJobRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public DateTime? CompletedDate { get; set; }
        public JobStatus Status { get; set; }

        public Guid EmployerId { get; set; }
        public Guid JobCategoryId { get; set; }
    }
}
