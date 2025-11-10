using AutoMapper;
using ParkingAPI.DTOs;
using ParkingAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParkingAPI.Profiles
{
    public class PatioProfile : Profile
    {
        public PatioProfile()
        {
            CreateMap<Patio, PatioDto>().ReverseMap();
        }
    }
}