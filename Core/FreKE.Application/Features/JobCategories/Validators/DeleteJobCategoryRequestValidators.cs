using FluentValidation;
using FreKE.Application.Features.JobCategories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.JobCategories.Validators
{
    public class DeleteJobCategoryRequestValidators : AbstractValidator<DeleteJobCategoryRequest>
    {
        public DeleteJobCategoryRequestValidators() 
        { 
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Kategori id boş olamaz.");
        }
    }
}
