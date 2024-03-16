using FluentValidation;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Validators.Bills
{
    public class BillCreateValidator: AbstractValidator<BillCreateDTO>
    {
        public BillCreateValidator()
        {
            RuleFor(b => b.Amount)
                .NotEmpty().WithMessage("Amount cannot be empty")
                .GreaterThanOrEqualTo(0).WithMessage("Amount must be greater than or equal to 0");

            RuleFor(b => b.PaidStatus)
                .NotEmpty().WithMessage("PaidStatus cannot be empty");

            RuleFor(b => b.GuestId)
                .NotEmpty().WithMessage("GuestId cannot be empty");
            //property ile vermeli
        }
    }
}
