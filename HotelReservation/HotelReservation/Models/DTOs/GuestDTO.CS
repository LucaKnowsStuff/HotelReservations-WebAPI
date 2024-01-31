namespace HotelReservation.Models.DTOs
{
    public class GuestDTO
    {
        public Guid GuestId { get; set; }
        public string GuestName { get; set; }
        public string GuestPhoneNumber { get; set; }
        public string? GuestPreferences { get; set; }
    }
}
