namespace HotelReservation.Models.Domain
{
    public class Room : BaseMetadataEntity
    {
        public Guid RoomId { get; set; }
        public double PricePerNight { get; set; }
        public string RoomType { get; set; }
        public string RoomDescription { get; set; }


    }
}
