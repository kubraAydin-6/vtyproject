using FluentValidation;
using FreKE.Application.Features.Likes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Likes.Validators
{
    public class DeleteLikeRequestValidators : AbstractValidator<DeleteLikeRequest>
    {
        public DeleteLikeRequestValidators()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Beğeni id si boş olamaz.");
        }
    }
}
