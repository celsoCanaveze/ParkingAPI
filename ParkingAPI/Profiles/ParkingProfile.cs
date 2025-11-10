using AutoMapper;
using ParkingAPI.DTOs;
using ParkingAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParkingAPI.Profiles
{
    public class ParkingProfile : Profile
    {
        public ParkingProfile()
        {
            CreateMap<Parking, ParkingDto>().ReverseMap();
        }
    }
}