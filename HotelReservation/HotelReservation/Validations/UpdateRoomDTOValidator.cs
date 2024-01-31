using FluentValidation;
using HotelReservation.Models.DTOs;

namespace HotelReservation.Validations
{
    public class UpdateRoomDTOValidator : AbstractValidator<UpdateRoomDTO>
    {
        public UpdateRoomDTOValidator()
        {
            RuleFor(updateRoomDTO => updateRoomDTO.PricePerNight)
                .NotEmpty().WithMessage("PricePerNight is a required field!")
                .GreaterThanOrEqualTo(0).WithMessage("PricePerNight cannot be negative!");

            RuleFor(updateRoomDTO => updateRoomDTO.RoomType)
                .NotEmpty().WithMessage("RoomType is a required field!")
                .MaximumLength(50).WithMessage("RoomType must be shorter than 50 charcters!");

            RuleFor(updateRoomDTO => updateRoomDTO.RoomDescription)
                .NotEmpty().WithMessage("RoomDescription is a required field!")
                .MinimumLength(10).WithMessage("RoomDescription must be longer than 10 charcters!")
                .MaximumLength(250).WithMessage("RoomDescription must be shorter than 250 charcters!");
        }
    }
}
