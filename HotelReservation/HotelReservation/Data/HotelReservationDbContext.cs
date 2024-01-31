using HotelReservation.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Data
{
    public class HotelReservationDbContext : DbContext
    {

        public HotelReservationDbContext(DbContextOptions<HotelReservationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Guest>(eb => {
                eb.HasKey(e => e.GuestId);
            });
            modelBuilder.Entity<Room>(eb => {
                eb.HasKey(e => e.RoomId);
            });
            modelBuilder.Entity<Reservation>(eb => {
                eb.HasKey(e => e.ReservationId);
                eb.HasOne(r => r.Guest).WithMany().HasForeignKey(r => r.GuestId).IsRequired();
                eb.HasOne(r => r.Room).WithMany().HasForeignKey(r => r.RoomId).IsRequired();
            });
        }
    }
}
