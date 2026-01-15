using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Users.DTOs
{
    public class UserCommentDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public Guid CommentUserId { get; set; }
        public string CommentUserName { get; set; }
        public string CommentUserSurname { get; set; }
        public Guid CommentId { get; set; }
        public string Content { get; set; }

    }
}
