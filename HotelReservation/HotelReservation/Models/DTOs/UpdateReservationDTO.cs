namespace HotelReservation.Models.DTOs
{
    public class UpdateReservationDTO
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Guid RoomId { get; set; }
        public Guid GuestId { get; set; }
    }
}
