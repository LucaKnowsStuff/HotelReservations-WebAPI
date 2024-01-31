namespace HotelReservation.Models.DTOs
{
    public class UpdateGuestDTO
    {
        public string GuestName { get; set; }
        public string GuestPhoneNumber { get; set; }
        public string? GuestPreferences { get; set; }
    }
}
