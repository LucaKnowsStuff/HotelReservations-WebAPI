using HotelReservation.Data;
using HotelReservation.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Repositories
{


    
    public class GuestRepository : BaseRepository<Guest> , IGuestRepository
    {

        public GuestRepository(HotelReservationDbContext dbContext) : base(dbContext)
        {
        
        }

    
    }
}
