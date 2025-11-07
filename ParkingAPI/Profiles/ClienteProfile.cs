using AutoMapper;
using ParkingApi.DTOs;
using ParkingApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParkingApi.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
        }
    }
}