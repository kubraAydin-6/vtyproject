using FreKE.Domain.Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Users.DTOs
{
    public class GetUserJobTakenDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public DateTime createdDate { get; set; }
        public JobStatus? Status { get; set; }

        public Guid? EmployerId { get; set; }
        public Guid? JobCategoryId { get; set; }
    }
}
