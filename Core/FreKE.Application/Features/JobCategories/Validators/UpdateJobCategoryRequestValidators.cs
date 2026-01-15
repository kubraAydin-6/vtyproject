using FluentValidation;
using FreKE.Application.Features.JobCategories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.JobCategories.Validators
{
    public class UpdateJobCategoryRequestValidators : AbstractValidator<UpdateJobCategoryRequest>
    {
        public UpdateJobCategoryRequestValidators()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Kategori id boş olamaz.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kategori adı 100 karakterden fazla olamaz.");
        }
    }
}
