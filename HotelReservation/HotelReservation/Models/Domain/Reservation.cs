namespace HotelReservation.Models.Domain
{
    public class Reservation : BaseMetadataEntity
    {
        public Guid ReservationId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool IsActive { get; set; }
        public Guid GuestId { get; set; }
        public Guid RoomId { get; set; }

        //Navigation Proprities
        public Guest Guest { get; set; }
        public Room Room { get; set; }

    }
}
