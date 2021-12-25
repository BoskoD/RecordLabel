namespace ContentManagement.Domain.Artists.Mappings;

using ContentManagement.Dtos.Artist;
using AutoMapper;
using ContentManagement.Domain.Artists;

public class ArtistProfile : Profile
{
    public ArtistProfile()
    {
        //createmap<to this, from this>
        CreateMap<Artist, ArtistDto>()
            .ReverseMap();
        CreateMap<ArtistForCreationDto, Artist>();
        CreateMap<ArtistForUpdateDto, Artist>()
            .ReverseMap();
    }
}