using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Jobs.DTOs
{
    public class JobProfileListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public string Status { get; set; }
        public string JobCategoryName { get; set; }
        public Guid JobCategoryId { get; set; }
        public string EmployerName { get; set; }
        public string EmployerSurname { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
