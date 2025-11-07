using AutoMapper;
using ParkingApi.DTOs;
using ParkingApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParkingApi.Profiles
{
    public class MotoProfile : Profile
    {
        public MotoProfile()
        {
            CreateMap<Moto, MotoDto>().ReverseMap();
        }
    }
}