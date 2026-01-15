using FluentValidation;
using FreKE.Application.Features.JobCategories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.JobCategories.Validators
{
    public class CreateJobCategoryRequestValidators : AbstractValidator<CreateJobCategoryRequest>
    {
        public CreateJobCategoryRequestValidators()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kategori adı 100 karakterden fazla olamaz.");
        }
    }
}
