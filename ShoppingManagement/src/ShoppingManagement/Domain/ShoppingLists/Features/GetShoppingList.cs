namespace ShoppingManagement.Domain.ShoppingLists.Features;

using ShoppingManagement.Dtos.ShoppingList;
using ShoppingManagement.Exceptions;
using ShoppingManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class GetShoppingList
{
    public class ShoppingListQuery : IRequest<ShoppingListDto>
    {
        public Guid Id { get; set; }

        public ShoppingListQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<ShoppingListQuery, ShoppingListDto>
    {
        private readonly ShoppingDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ShoppingDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<ShoppingListDto> Handle(ShoppingListQuery request, CancellationToken cancellationToken)
        {
            var result = await _db.ShoppingLists
                .AsNoTracking()
                .ProjectTo<ShoppingListDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (result == null)
                throw new NotFoundException("ShoppingList", request.Id);

            return result;
        }
    }
}