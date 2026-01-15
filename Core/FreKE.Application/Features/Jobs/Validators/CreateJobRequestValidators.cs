using FluentValidation;
using FreKE.Application.Features.Jobs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Jobs.Validators
{
    public class CreateJobRequestValidators : AbstractValidator<CreateJobRequest>
    {
        public CreateJobRequestValidators() 
        { 
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık kısmı boş olamaz.")
                .MaximumLength(100).WithMessage("Başlık karakteri 100 den fazla olamaz.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama kısmı boş olamaz")
                .MaximumLength(1000).WithMessage("Açıklama 1000 karakterden az olmalı.");
            RuleFor(x => x.Budget)
                .GreaterThan(0).WithMessage("Bütçe 0 dan büyük olmalı.");
            RuleFor(x => x.EmployerId)
                .NotEmpty().WithMessage("İş veren id boş olamaz.");
            RuleFor(x => x.JobCategoryId)
                .NotEmpty().WithMessage("işkategori id boş olamaz.");
        }
    }
}
