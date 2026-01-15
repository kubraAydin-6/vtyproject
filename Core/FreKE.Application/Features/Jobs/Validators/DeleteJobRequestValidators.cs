using FluentValidation;
using FreKE.Application.Features.Jobs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Jobs.Validators
{
    public class DeleteJobRequestValidators : AbstractValidator<DeleteJobRequest>
    {
        public DeleteJobRequestValidators()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("İş id si boş olamaz.");
        }
    }
}
