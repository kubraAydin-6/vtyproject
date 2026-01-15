using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Comments.DTOs
{
    public class UpdateCommentRequest
    {
        public Guid Id { get; set; }
        public string Content { get; set; }

    }
}
