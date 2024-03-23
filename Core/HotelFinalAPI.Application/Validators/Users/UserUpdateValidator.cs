using FluentValidation;
using HotelFinalAPI.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Validators.Users
{
    public class UserUpdateValidator:AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateValidator()
        {
            RuleFor(r => r.UserName)
               .NotNull().NotEmpty().WithMessage("Username cannot be empty.");
            RuleFor(r => r.Email)
                .NotNull().NotEmpty().WithMessage("Enter valid email.");
            RuleFor(r => r.FirstName)
                .NotNull().NotEmpty().WithMessage("Firstname cannot be empty")
                .MaximumLength(50).WithMessage("Name cannot be more than 50 characters");
            RuleFor(r => r.LastName)
                .NotNull().NotEmpty().WithMessage("Lastname cannot be empty")
                .MaximumLength(50).WithMessage("Surname cannot be more than 50 characters");
        }
    }
}
