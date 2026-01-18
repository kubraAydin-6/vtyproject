using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Jobs.DTOs
{
    public class GetJobDateTotalDto
    {
        public DateTime Date { get; set; }
        public int JobTotal { get; set; }
        public string CategoryName { get; set; }
    }
}
