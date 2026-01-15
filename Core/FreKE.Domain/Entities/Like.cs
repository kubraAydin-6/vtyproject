using FreKE.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Domain.Entities
{
    public class Like : BaseEntity
    {
        public Guid LikedById { get; set; }   // Beğeniyi yapan
        public User LikedBy { get; set; }
        public Guid LikedUserId { get; set; }  //beğeni alan
        public User LikedUser { get; set; }

    }
}
