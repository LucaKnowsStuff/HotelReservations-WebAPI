using FluentValidation;
using HotelReservation.Models.DTOs;

namespace HotelReservation.Validations
{
    public class UpdateGuestDTOValidator : AbstractValidator<UpdateGuestDTO>
    {
        public UpdateGuestDTOValidator()
        {
            RuleFor(updateGuestDTO => updateGuestDTO.GuestName)
                .NotEmpty().WithMessage("GuesName is a required field!")
                .MinimumLength(5).WithMessage("GuestName needs to have at least 5 charcters!")
                .MaximumLength(50).WithMessage("GuestName can´t be bigger than 50 charcters");

            RuleFor(updateGuestDTO => updateGuestDTO.GuestPhoneNumber)
                .NotEmpty().WithMessage("GuestPhoneNumber is a required field!")
                .Length(9).WithMessage("GuestPhoneNumber needs to have 9 digits!")
                .Matches("^9\\d*$").WithMessage("GuestPhoneNumber has a reqired format of 9xxxxxxxx!");

            RuleFor(updateGuestDTO => updateGuestDTO.GuestPreferences)
                .MaximumLength(250).WithMessage("GuestPreferences can´t be bigger than 250 charcters|");
        }
    }
}
