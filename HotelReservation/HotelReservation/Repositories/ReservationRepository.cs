using HotelReservation.Data;
using HotelReservation.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(HotelReservationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> CancelReservation(Guid id)
        {
            var reservation = await _dbContext.Reservations.FindAsync(id);
            if (reservation == null || reservation.IsDeleted || !reservation.IsActive)
            {
                return false;
            }

            reservation.IsActive = false;
            _dbContext.Reservations.Update(reservation);
            return true;

        }
    }
}