using AutoMapper;
using HotelReservation.Models.Domain;
using HotelReservation.Models.DTOs;

namespace HotelReservation.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Guest, AddGuestDTO>().ReverseMap();
            CreateMap<Guest, GuestDTO>().ReverseMap();
            CreateMap<Guest, UpdateGuestDTO>().ReverseMap();
            CreateMap<Room , RoomDTO>().ReverseMap();
            CreateMap<Room , UpdateRoomDTO>().ReverseMap();
            CreateMap<Room , AddRoomDTO>().ReverseMap();
            CreateMap<Reservation, ReservationDTO>().ReverseMap();
            CreateMap<Reservation , AddReservationDTO>().ReverseMap();
            CreateMap<Reservation,  UpdateReservationDTO>().ReverseMap();
        }
    }
}
