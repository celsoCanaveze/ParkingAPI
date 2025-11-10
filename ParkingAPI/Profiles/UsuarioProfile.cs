using AutoMapper;
using ParkingAPI.DTOs;
using ParkingAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParkingAPI.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}