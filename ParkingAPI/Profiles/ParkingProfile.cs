using AutoMapper;
using ParkingApi.DTOs;
using ParkingApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParkingApi.Profiles
{
    public class ParkingProfile : Profile
    {
        public ParkingProfile()
        {
            CreateMap<Parking, ParkingDto>().ReverseMap();
        }
    }
}