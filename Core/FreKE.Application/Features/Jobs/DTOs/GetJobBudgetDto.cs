using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Jobs.DTOs
{
    public class GetJobBudgetDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public decimal Budget { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
