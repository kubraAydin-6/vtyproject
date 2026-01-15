using FluentValidation;
using FreKE.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.Users.Validators
{
    public class CreateUserRequestValidators : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidators() 
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Kullanıcı adı boş olamaz.")
                .MaximumLength(100)
                .WithMessage("Kullanıcı adı en fazla 100 karakter olabilir.");

            RuleFor(p => p.Surname)
                .NotEmpty()
                .WithMessage("Kullanıcı soyadı boş olamaz.")
                .MaximumLength(100)
                .WithMessage("Kullanıcı soyadı en fazla 100 karakter olabilir.");

            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("Email boş olamaz.")
                .EmailAddress()
                .WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(p => p.Phone)
                .NotEmpty()
                .WithMessage("Telefon numarası boş olamaz.")
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Geçerli bir telefon numarası giriniz.");
        }
    }
}
