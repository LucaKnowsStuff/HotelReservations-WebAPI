using HotelReservation.Data;
using HotelReservation.Models.Domain;

namespace HotelReservation.Repositories
{
    public class RoomRepository :BaseRepository<Room> ,IRoomRepository
    {
        public RoomRepository(HotelReservationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
