using FluentValidation;
using FreKE.Application.Features.PriceOffers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.PriceOffers.Validators
{
    public class DeletePriceOfferRequestValidators : AbstractValidator<DeletePriceOfferRequest>
    {
        public DeletePriceOfferRequestValidators() 
        {
            RuleFor(P => P.Id)
                .NotEmpty()
                .WithMessage("Fiyat Teklifi Id Boş olamaz");
        }
    }
}
