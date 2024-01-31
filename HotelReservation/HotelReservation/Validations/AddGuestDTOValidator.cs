using FluentValidation;
using HotelReservation.Models.DTOs;

namespace HotelReservation.Validations
{
    public class AddGuestDTOValidator : AbstractValidator<AddGuestDTO>
    {
        public AddGuestDTOValidator()
        {
            RuleFor(addGuestDTO => addGuestDTO.GuestName)
                .NotEmpty().WithMessage("GuesName is a required field!")
                .MinimumLength(5).WithMessage("GuestName needs to have at least 5 charcters!")
                .MaximumLength(50).WithMessage("GuestName can´t be bigger than 50 charcters");

            RuleFor(addGuestDTO => addGuestDTO.GuestPhoneNumber)
                .NotEmpty().WithMessage("GuestPhoneNumber is a required field!")
                .Length(9).WithMessage("GuestPhoneNumber needs to have 9 digits!")
                .Matches("^9\\d*$").WithMessage("GuestPhoneNumber has a reqired format of 9xxxxxxxx!");

            RuleFor(addGuestDTO => addGuestDTO.GuestPreferences)
                .MaximumLength(250).WithMessage("GuestPreferences can´t be bigger than 250 charcters|");
        }
    }
}
