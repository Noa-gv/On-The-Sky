using AutoMapper;
using on_the_sky.core.Dto;
using on_the_sky.core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace on_the_sky.core
{
    public class MappingProfile: Profile
    {
       public MappingProfile() {

            CreateMap<Flight, FlightDto>().ReverseMap();
            CreateMap<Travel, TravelDto>().ReverseMap();
            CreateMap<Places, PlaceDto>().ReverseMap();
            CreateMap<Users, UserDto>();

        }

    }
}
