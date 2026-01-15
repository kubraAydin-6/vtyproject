using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Likes.DTOs
{
    public class CreateLikeRequest
    {
        public Guid LikedById { get; set; }
        public Guid LikedUserId { get; set; }
    }
}
