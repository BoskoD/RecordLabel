namespace ShoppingManagement.Domain.ShoppingLists.Features;

using ShoppingManagement.Domain.ShoppingLists;
using ShoppingManagement.Dtos.ShoppingList;
using ShoppingManagement.Exceptions;
using ShoppingManagement.Databases;
using ShoppingManagement.Wrappers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;
using System.Threading;
using System.Threading.Tasks;

public static class GetShoppingListList
{
    public class ShoppingListListQuery : IRequest<PagedList<ShoppingListDto>>
    {
        public ShoppingListParametersDto QueryParameters { get; set; }

        public ShoppingListListQuery(ShoppingListParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<ShoppingListListQuery, PagedList<ShoppingListDto>>
    {
        private readonly ShoppingDbContext _db;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(ShoppingDbContext db, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _db = db;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<ShoppingListDto>> Handle(ShoppingListListQuery request, CancellationToken cancellationToken)
        {
            var collection = _db.ShoppingLists
                as IQueryable<ShoppingList>;

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<ShoppingListDto>(_mapper.ConfigurationProvider);

            return await PagedList<ShoppingListDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}