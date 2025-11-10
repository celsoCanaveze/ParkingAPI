using AutoMapper;
using ParkingAPI.DTOs;
using ParkingAPI.Models;

namespace ParkingAPI.Profiles
{
    public class ReservaProfile : Profile
    {
        public ReservaProfile()
        {
            CreateMap<Reserva, ReservaDto>().ReverseMap();
        }
    }
}
