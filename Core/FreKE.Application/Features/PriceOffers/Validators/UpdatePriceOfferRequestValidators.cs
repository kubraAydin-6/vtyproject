using FluentValidation;
using FreKE.Application.Features.PriceOffers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Application.Features.PriceOffers.Validators
{
    public class UpdatePriceOfferRequestValidators : AbstractValidator<UpdatePriceOfferRequest>
    {
        public UpdatePriceOfferRequestValidators() 
        { 
            RuleFor(P => P.Id)
                .NotEmpty()
                .WithMessage("Fiyat Teklifi Id Boş olamaz");
            RuleFor(P => P.OfferedPrice)
                .GreaterThan(0)
                .WithMessage("Teklif Edilen Fiyat 0'dan büyük olmalıdır");
            RuleFor(P => P.WorkerId)
                .NotEmpty()
                .WithMessage("Çalışan Id Boş olamaz");
            RuleFor(P => P.JobId)
                .NotEmpty()
                .WithMessage("İş Id Boş olamaz");
        }
    }
}
