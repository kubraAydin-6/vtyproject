using FreKE.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Domain.Entities
{
    public class JobCategory : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
}
