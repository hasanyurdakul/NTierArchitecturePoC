using AutoMapper;
using Music.API.DTOs;
using Music.Core.Model;

namespace Music.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Track, TrackDTO>().ReverseMap();
            CreateMap<Artist, ArtistDTO>().ReverseMap();
            CreateMap<SaveTrackDTO, Track>();
            CreateMap<SaveArtistDTO, Artist>();
        }
    }
}
