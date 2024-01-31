using System.Text.RegularExpressions;
using FluentValidation;
using HotelReservation.Models.DTOs;

namespace HotelReservation.Validations
{
    public class AddReservationDTOValidator : AbstractValidator<AddReservationDTO>
    {
        public AddReservationDTOValidator()
        {
            RuleFor(reservationDTO => reservationDTO.CheckInDate)
                .NotEmpty().WithMessage("CheckInDate is a required field!")
                .Must(BeValidCheckIn).WithMessage("CheckInDate must be at least 1 day in the future from today and not more than a year in the future!");

            RuleFor(reservationDTO => reservationDTO.CheckOutDate)
                .NotEmpty().WithMessage("CheckOutDate is required.")
                .Must((reservationDTO, checkOutDate) => BeValidCheckOut(reservationDTO.CheckInDate, checkOutDate)).WithMessage("CheckOutDate must be after or the same as CheckInDate!");

            RuleFor(reservationDTO => reservationDTO.GuestId)
                .NotEmpty().WithMessage("GuestId is a requried field!");

            RuleFor(reservationDTO => reservationDTO.RoomId)
                .NotEmpty().WithMessage("RoomId is a required field!");
        }



        private bool BeValidCheckOut(DateTime checkInDate, DateTime checkOutDate)
        {
            return checkOutDate >= checkInDate;
        }
        private bool BeValidCheckIn(DateTime date)
        {
            var today = DateTime.Today.Date;
            return date > today && date <= today.AddYears(1);
        }
    }
}
