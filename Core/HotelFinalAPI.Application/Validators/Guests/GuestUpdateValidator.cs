using FluentValidation;
using HotelFinalAPI.Application.DTOs.GuestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Validators.Guests
{
    public class GuestUpdateValidator:AbstractValidator<GuestUpdateDTO>
    {
        public GuestUpdateValidator()
        {
            RuleFor(g => g.FirstName)
               .NotEmpty().WithMessage("First name is required.")
               .MinimumLength(2).WithMessage("First name cannot be less than 2 characters")
               .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(g => g.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).WithMessage("Surname cannot be less than 2 characters")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(g => g.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(g => g.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.");
        }
    }
}
