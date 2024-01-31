using HotelReservation.Models.Domain;

namespace HotelReservation.Repositories
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
         
        Task<bool> CancelReservation(Guid id);

    }
}
