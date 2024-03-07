using FluentValidation;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Validators.Bills
{
    public class BillUpdateValidator:AbstractValidator<BillUpdateDTO>
    {
        public BillUpdateValidator()
        {
            RuleFor(b => b.Amount)
                .NotEmpty().WithMessage("Amount cannot be empty")
                .GreaterThan(0).WithMessage("Amount must be greater than 0");

            RuleFor(b => b.PaidStatus)
                .NotEmpty().WithMessage("PaidStatus cannot be empty")
                .NotNull().WithMessage("PaidStatus cannot be null");
        }
    }
}
