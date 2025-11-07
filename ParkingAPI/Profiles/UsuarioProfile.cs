using AutoMapper;
using ParkingApi.DTOs;
using ParkingApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParkingApi.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}