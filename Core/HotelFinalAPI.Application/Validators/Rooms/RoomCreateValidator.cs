using FluentValidation;
using HotelFinalAPI.Application.DTOs.RoomDTOs;
using HotelFinalAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Validators.Rooms
{
    public class RoomCreateValidator : AbstractValidator<RoomCreateDTO>
    {
        public RoomCreateValidator()
        {
            RuleFor(room => room.RoomNumber)
                .NotEmpty().WithMessage("Room number is required.")
                .MaximumLength(50).WithMessage("Room number cannot exceed 50 characters.");

            RuleFor(room => room.RoomType)
                .Must(BeValidRoomType).WithMessage("Invalid room type.");

            RuleFor(room => room.Status)
                .NotEmpty().WithMessage("Room status is required.")
                .MaximumLength(50).WithMessage("Room status cannot exceed 50 characters.");

            RuleFor(r => r.Price)
                .NotEmpty().WithMessage("Price cannot be null")
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0");

        }

        private bool BeValidRoomType(string roomType)
        {
            return Enum.IsDefined(typeof(RoomTypes), roomType);
        }
    }
}
