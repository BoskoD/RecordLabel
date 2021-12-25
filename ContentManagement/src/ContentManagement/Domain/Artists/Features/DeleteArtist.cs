namespace ContentManagement.Domain.Artists.Features;

using ContentManagement.Domain.Artists;
using ContentManagement.Dtos.Artist;
using ContentManagement.Exceptions;
using ContentManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class DeleteArtist
{
    public class DeleteArtistCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteArtistCommand(Guid artist)
        {
            Id = artist;
        }
    }

    public class Handler : IRequestHandler<DeleteArtistCommand, bool>
    {
        private readonly ContentDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ContentDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _db.Artists
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (recordToDelete == null)
                throw new NotFoundException("Artist", request.Id);

            _db.Artists.Remove(recordToDelete);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}