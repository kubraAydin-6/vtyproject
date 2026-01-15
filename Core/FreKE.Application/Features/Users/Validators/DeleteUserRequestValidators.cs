using FluentValidation;
using FreKE.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Users.Validators
{
    public class DeleteUserRequestValidators : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserRequestValidators()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("Kullanıcı Id'si boş olamaz.");
        }
    }
}
