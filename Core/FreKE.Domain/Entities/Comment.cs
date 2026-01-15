using FreKE.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Domain.Entities
{
    public class Comment: BaseEntity
    {
        public string Content { get; set; }


        public Guid CommentedById { get; set; }
        public User CommentedBy { get; set; }
        public Guid CommentedTargetId { get; set; }
        public User CommentedTarget { get; set; }
    }
}
