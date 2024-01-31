using System;
using HotelReservation.Data;
using HotelReservation.Repositories;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.UnitOfWork
{
    public class UOWork : IUOWork, IDisposable
    {

        public readonly HotelReservationDbContext _dbContex;
        public  IGuestRepository GuestRepository {  get; set; }

        public IReservationRepository ReservationRepository { get; set; }

        public IRoomRepository RoomRepository { get; set; }

       


        public UOWork(
            HotelReservationDbContext dbContex, 
            IGuestRepository guestRepository,
            IRoomRepository roomRepository,
            IReservationRepository reservationRepository)
        {

            _dbContex = dbContex;
            GuestRepository = guestRepository;
            RoomRepository = roomRepository;
            ReservationRepository = reservationRepository;
            
        }

        public void Dispose()
        {
            _dbContex.Dispose();
        }

        public async Task SaveAsync()
        {
           await _dbContex.SaveChangesAsync();
        }
    }
}
