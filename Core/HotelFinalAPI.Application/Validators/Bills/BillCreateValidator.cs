using FluentValidation;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Validators.Bills
{
    public class BillCreateValidator : AbstractValidator<BillCreateDTO>
    {
        public BillCreateValidator()
        {
            RuleFor(b => b.Amount)
                .NotEmpty().WithMessage("Amount cannot be empty")
                .GreaterThanOrEqualTo(0).WithMessage("Amount must be greater than or equal to 0");

            RuleFor(b => b.PaidStatus)
                        .NotNull().WithMessage("PaidStatus cannot be empty")
                        .Must(value => value == true || value == false)
                        .WithMessage("PaidStatus must be either true or false");

            RuleFor(b => b.GuestId)
                .NotEmpty().WithMessage("GuestId cannot be empty")
                .Must(BeValidGuid).WithMessage("Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).CustomMessage");
            
        }

        private bool BeValidGuid(string id)
        {
            return Guid.TryParse(id, out _);
        }
    }
}
