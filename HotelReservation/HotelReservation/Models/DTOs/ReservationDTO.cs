using HotelReservation.Models.Domain;

namespace HotelReservation.Models.DTOs
{
    public class ReservationDTO
    {
        public Guid ReservationId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool IsActive { get; set; }

        public Guest Guest { get; set; }
        public Room Room { get; set; }
    }
}
