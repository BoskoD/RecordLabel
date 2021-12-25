namespace ShoppingManagement.Domain.ShoppingLists.Features;

using ShoppingManagement.Domain.ShoppingLists;
using ShoppingManagement.Dtos.ShoppingList;
using ShoppingManagement.Exceptions;
using ShoppingManagement.Databases;
using ShoppingManagement.Domain.ShoppingLists.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class AddShoppingList
{
    public class AddShoppingListCommand : IRequest<ShoppingListDto>
    {
        public ShoppingListForCreationDto ShoppingListToAdd { get; set; }

        public AddShoppingListCommand(ShoppingListForCreationDto shoppingListToAdd)
        {
            ShoppingListToAdd = shoppingListToAdd;
        }
    }

    public class Handler : IRequestHandler<AddShoppingListCommand, ShoppingListDto>
    {
        private readonly ShoppingDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ShoppingDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<ShoppingListDto> Handle(AddShoppingListCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = _mapper.Map<ShoppingList> (request.ShoppingListToAdd);
            _db.ShoppingLists.Add(shoppingList);

            await _db.SaveChangesAsync(cancellationToken);

            return await _db.ShoppingLists
                .AsNoTracking()
                .ProjectTo<ShoppingListDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(s => s.Id == shoppingList.Id, cancellationToken);
        }
    }
}