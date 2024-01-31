using System.Net;
using HotelReservation.Data;
using HotelReservation.Repositories;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.UnitOfWork
{
    public interface IUOWork
    {
        IGuestRepository GuestRepository { get; }    
        IReservationRepository ReservationRepository { get;}
        IRoomRepository RoomRepository { get; }

        Task SaveAsync();

    }
}
