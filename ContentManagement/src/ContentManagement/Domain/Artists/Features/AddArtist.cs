namespace ContentManagement.Domain.Artists.Features;

using ContentManagement.Domain.Artists;
using ContentManagement.Dtos.Artist;
using ContentManagement.Exceptions;
using ContentManagement.Databases;
using ContentManagement.Domain.Artists.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class AddArtist
{
    public class AddArtistCommand : IRequest<ArtistDto>
    {
        public ArtistForCreationDto ArtistToAdd { get; set; }

        public AddArtistCommand(ArtistForCreationDto artistToAdd)
        {
            ArtistToAdd = artistToAdd;
        }
    }

    public class Handler : IRequestHandler<AddArtistCommand, ArtistDto>
    {
        private readonly ContentDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ContentDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<ArtistDto> Handle(AddArtistCommand request, CancellationToken cancellationToken)
        {
            var artist = _mapper.Map<Artist> (request.ArtistToAdd);
            _db.Artists.Add(artist);

            await _db.SaveChangesAsync(cancellationToken);

            return await _db.Artists
                .AsNoTracking()
                .ProjectTo<ArtistDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == artist.Id, cancellationToken);
        }
    }
}