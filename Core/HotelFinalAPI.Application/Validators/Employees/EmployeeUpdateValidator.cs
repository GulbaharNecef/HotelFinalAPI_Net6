using FluentValidation;
using HotelFinalAPI.Application.DTOs.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Validators.Employees
{
    public class EmployeeUpdateValidator:AbstractValidator<EmployeeUpdateDTO>
    {
        public EmployeeUpdateValidator()
        {
            RuleFor(e => e.FirstName)
                .NotEmpty().WithMessage("First name cannot be empty")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

            RuleFor(e => e.LastName)
                .NotEmpty().WithMessage("Last name cannot be empty")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid email address")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(e => e.Role)
                .NotEmpty().WithMessage("Role cannot be empty")
                .MaximumLength(100).WithMessage("Role cannot exceed 100 characters");
        }
    }
}
