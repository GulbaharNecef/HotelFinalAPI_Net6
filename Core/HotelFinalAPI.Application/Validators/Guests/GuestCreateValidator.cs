﻿using FluentValidation;
using HotelFinalAPI.Application.DTOs.GuestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Validators.Guests
{
    public class GuestCreateValidator : AbstractValidator<GuestCreateDTO>
    {
        public GuestCreateValidator()
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
            //.Matches(@"^\+\d{1,3}\d{3,14}$").WithMessage("Invalid phone number format.");

            RuleFor(g => g.DateOfBirth)
                .NotEmpty().WithMessage("DateOfBirth is required.")
                .Must(BeValidDate).WithMessage("Guest must be elder than 18");
            RuleFor(g => g.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country cannot exceed 50 characters.");

            RuleFor(g => g.SpecialRequests)
                .MaximumLength(500).WithMessage("Special request cannot exceed 500 characters.");

            RuleFor(g=>g.EmergencyContact)
                .MaximumLength(100).WithMessage("Special request cannot exceed 100 characters.");


        }

        private bool BeValidDate(DateTime date)
        {
            return date <= DateTime.Now.AddYears(-18);
        }

        //private void validateEmail(string email)
        //{
        //    MailAddress mailAddress = new MailAddress(email);
        //}
    }
}
