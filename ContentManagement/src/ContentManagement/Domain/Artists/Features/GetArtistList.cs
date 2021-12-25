namespace ContentManagement.Domain.Artists.Features;

using ContentManagement.Domain.Artists;
using ContentManagement.Dtos.Artist;
using ContentManagement.Exceptions;
using ContentManagement.Databases;
using ContentManagement.Wrappers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;
using System.Threading;
using System.Threading.Tasks;

public static class GetArtistList
{
    public class ArtistListQuery : IRequest<PagedList<ArtistDto>>
    {
        public ArtistParametersDto QueryParameters { get; set; }

        public ArtistListQuery(ArtistParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<ArtistListQuery, PagedList<ArtistDto>>
    {
        private readonly ContentDbContext _db;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(ContentDbContext db, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _db = db;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<ArtistDto>> Handle(ArtistListQuery request, CancellationToken cancellationToken)
        {
            var collection = _db.Artists
                as IQueryable<Artist>;

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<ArtistDto>(_mapper.ConfigurationProvider);

            return await PagedList<ArtistDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}