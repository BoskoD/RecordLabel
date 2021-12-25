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

public static class UpdateShoppingList
{
    public class UpdateShoppingListCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public ShoppingListForUpdateDto ShoppingListToUpdate { get; set; }

        public UpdateShoppingListCommand(Guid shoppingList, ShoppingListForUpdateDto newShoppingListData)
        {
            Id = shoppingList;
            ShoppingListToUpdate = newShoppingListData;
        }
    }

    public class Handler : IRequestHandler<UpdateShoppingListCommand, bool>
    {
        private readonly ShoppingDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ShoppingDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(UpdateShoppingListCommand request, CancellationToken cancellationToken)
        {
            var shoppingListToUpdate = await _db.ShoppingLists
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (shoppingListToUpdate == null)
                throw new NotFoundException("ShoppingList", request.Id);

            _mapper.Map(request.ShoppingListToUpdate, shoppingListToUpdate);

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}