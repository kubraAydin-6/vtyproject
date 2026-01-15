using FluentValidation;
using FreKE.Application.Features.Comments.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Comments.Validators
{
    public class UpdateCommentRequestValidators : AbstractValidator<UpdateCommentRequest>
    {
        public UpdateCommentRequestValidators() 
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Yorum id si boş olamaz.");
            RuleFor(c => c.Content)
                .NotEmpty().WithMessage("Yorum içeriği boş olamaz.")
                .MaximumLength(1000).WithMessage("Yorum içeriği 1000 karakterden az olmalı.");
        }
    }
}
