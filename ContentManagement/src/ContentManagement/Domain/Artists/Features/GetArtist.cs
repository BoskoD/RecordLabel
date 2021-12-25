namespace ContentManagement.Domain.Artists.Features;

using ContentManagement.Dtos.Artist;
using ContentManagement.Exceptions;
using ContentManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class GetArtist
{
    public class ArtistQuery : IRequest<ArtistDto>
    {
        public Guid Id { get; set; }

        public ArtistQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<ArtistQuery, ArtistDto>
    {
        private readonly ContentDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ContentDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<ArtistDto> Handle(ArtistQuery request, CancellationToken cancellationToken)
        {
            var result = await _db.Artists
                .AsNoTracking()
                .ProjectTo<ArtistDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (result == null)
                throw new NotFoundException("Artist", request.Id);

            return result;
        }
    }
}