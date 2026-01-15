using FreKE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Comments.DTOs
{
    public class CreateCommentRequest
    {
        public string Content { get; set; }

        public Guid CommentedById { get; set; }
        public Guid CommentedTargetId { get; set; }
    }
}
