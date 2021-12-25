namespace ShoppingManagement.Domain.ShoppingLists.Features;

using ShoppingManagement.Domain.ShoppingLists;
using ShoppingManagement.Dtos.ShoppingList;
using ShoppingManagement.Exceptions;
using ShoppingManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class DeleteShoppingList
{
    public class DeleteShoppingListCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteShoppingListCommand(Guid shoppingList)
        {
            Id = shoppingList;
        }
    }

    public class Handler : IRequestHandler<DeleteShoppingListCommand, bool>
    {
        private readonly ShoppingDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ShoppingDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(DeleteShoppingListCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _db.ShoppingLists
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (recordToDelete == null)
                throw new NotFoundException("ShoppingList", request.Id);

            _db.ShoppingLists.Remove(recordToDelete);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}