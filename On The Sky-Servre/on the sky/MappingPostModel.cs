using AutoMapper;
using on_the_sky.models;

namespace on_the_sky
{
    public class MappingPostModel:Profile
    {
        public MappingPostModel() {
            CreateMap<FlightPostModel , Flight>().ReverseMap();
            CreateMap<PlacesPostModel , Places>().ReverseMap();
            CreateMap<TravelPostModel, Travel>().ReverseMap();

        }

    }
}

