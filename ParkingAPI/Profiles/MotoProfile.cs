using AutoMapper;
using ParkingAPI.DTOs;
using ParkingAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParkingAPI.Profiles
{
    public class MotoProfile : Profile
    {
        public MotoProfile()
        {
            CreateMap<Moto, MotoDto>().ReverseMap();
        }
    }
}