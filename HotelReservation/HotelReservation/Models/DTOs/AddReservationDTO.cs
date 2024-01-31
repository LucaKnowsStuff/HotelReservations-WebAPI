namespace HotelReservation.Models.DTOs
{
    public class AddReservationDTO
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Guid GuestId { get; set; }
        public Guid RoomId { get; set; }
    }
}
