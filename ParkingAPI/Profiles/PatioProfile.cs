using AutoMapper;
using ParkingApi.DTOs;
using ParkingApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParkingApi.Profiles
{
    public class PatioProfile : Profile
    {
        public PatioProfile()
        {
            CreateMap<Patio, PatioDto>().ReverseMap();
        }
    }
}