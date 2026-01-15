using FreKE.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }


        public ICollection<Job> PostedJobs { get; set; }  // Job ilişkisi (İşveren tarafından açılmış işler)
        public ICollection<PriceOffer> PriceOffers { get; set; }  // PriceOffer ilişkisi (Bir kullanıcının verdiği teklifler)
        public ICollection<Comment> CommentsGiven { get; set; }// Comment ilişkisi (Bir kullanıcının yaptığı yorumlar)
        public ICollection<Comment> CommentsReceived { get; set; } // Comment ilişkisi (Bir kullanıcının aldığı yorumlar)
        public ICollection<Like> LikesGiven { get; set; }  // Like ilişkisi (Bir kullanıcının verdiği beğeniler)
        public ICollection<Like> LikesReceived { get; set; }  // Like ilişkisi (Bir kullanıcının aldığı beğeniler)

    }
}
