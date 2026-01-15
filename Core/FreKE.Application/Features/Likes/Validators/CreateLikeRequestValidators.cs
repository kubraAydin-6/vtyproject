using FluentValidation;
using FreKE.Application.Features.Likes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Likes.Validators
{
    public class CreateLikeRequestValidators : AbstractValidator<CreateLikeRequest>
    {
        public CreateLikeRequestValidators() 
        { 
            RuleFor(x => x.LikedById)
                .NotEmpty().WithMessage("Beğeni alan kişinin id si boş olamaz.");
            RuleFor(x => x.LikedUserId)
                .NotEmpty().WithMessage("Beğeni yapan kişinin id si boş olamaz.");
        }
    }
}
