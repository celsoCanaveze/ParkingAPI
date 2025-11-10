using AutoMapper;
using ParkingAPI.DTOs;
using ParkingAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParkingAPI.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
        }
    }
}