using FluentValidation;
using HotelFinalAPI.Application.DTOs.ReservationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Validators.Reservations
{
    public class ReservationUpdateValidator : AbstractValidator<ReservationUpdateDTO>
    {
        public ReservationUpdateValidator()
        {
            RuleFor(r => r.GuestId)
                .NotEmpty().WithMessage("GuestId cannot be empty")
                .Must(guestId => Guid.TryParse(guestId, out _)).WithMessage("Guest Id must be a valid Guid");

            RuleFor(r => r.RoomId)
                .NotEmpty().WithMessage("Room is cannot be empty")
                .Must(roomId => Guid.TryParse(roomId, out _)).WithMessage("Room Id must be valid Guid");

            RuleFor(r => r.CheckInDate)
                .NotEmpty().WithMessage("Check in date cannot be empty")
                .Must(BeValidDate).WithMessage("Check-in date must be in the future");

            RuleFor(r => r.CheckOutDate)
                .NotEmpty().WithMessage("Check out date cannot be empty");
        }

        private bool BeValidDate(DateTime date)
        {
            //return date.Date != DateTime.MinValue;
            return date.Date >= DateTime.Now;
        }
    }
}
