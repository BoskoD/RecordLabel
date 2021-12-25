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

public static class UpdateArtist
{
    public class UpdateArtistCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public ArtistForUpdateDto ArtistToUpdate { get; set; }

        public UpdateArtistCommand(Guid artist, ArtistForUpdateDto newArtistData)
        {
            Id = artist;
            ArtistToUpdate = newArtistData;
        }
    }

    public class Handler : IRequestHandler<UpdateArtistCommand, bool>
    {
        private readonly ContentDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ContentDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
        {
            var artistToUpdate = await _db.Artists
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (artistToUpdate == null)
                throw new NotFoundException("Artist", request.Id);

            _mapper.Map(request.ArtistToUpdate, artistToUpdate);

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}