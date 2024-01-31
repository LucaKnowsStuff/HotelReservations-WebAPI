namespace HotelReservation.Models.DTOs
{
    public class RoomDTO
    {
        public Guid RoomId { get; set; }
        public double PricePerNight { get; set; }
        public string RoomType { get; set; }
        public string RoomDescription { get; set; }
    }
}
