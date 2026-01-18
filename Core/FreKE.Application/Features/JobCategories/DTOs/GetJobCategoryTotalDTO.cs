using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.JobCategories.DTOs
{
    public class GetJobCategoryTotalDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TotalJobs { get; set; }
    }
}
