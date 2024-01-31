using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservation.Models.Domain
{
    public class Guest : BaseMetadataEntity
    {
        
        public Guid GuestId { get; set; }
        public string GuestName { get; set; }
        public string GuestPhoneNumber { get; set; }
        public string? GuestPreferences { get; set; }     

    }
}
