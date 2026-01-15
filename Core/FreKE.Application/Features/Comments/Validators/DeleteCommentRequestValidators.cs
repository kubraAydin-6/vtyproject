using FluentValidation;
using FreKE.Application.Features.Comments.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Comments.Validators
{
    public class DeleteCommentRequestValidators : AbstractValidator<DeleteCommentRequest>
    {
        public DeleteCommentRequestValidators() 
        { 
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Yorum id si boş olamaz.");
        }
    }
}
