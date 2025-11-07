using AutoMapper;
using ParkingApi.DTOs;
using ParkingApi.Models;

namespace ParkingApi.Profiles
{
    public class ReservaProfile : Profile
    {
        public ReservaProfile()
        {
            CreateMap<Reserva, ReservaDto>().ReverseMap();
        }
    }
}
