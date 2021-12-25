namespace ShoppingManagement.Domain.ShoppingLists.Mappings;

using ShoppingManagement.Dtos.ShoppingList;
using AutoMapper;
using ShoppingManagement.Domain.ShoppingLists;

public class ShoppingListProfile : Profile
{
    public ShoppingListProfile()
    {
        //createmap<to this, from this>
        CreateMap<ShoppingList, ShoppingListDto>()
            .ReverseMap();
        CreateMap<ShoppingListForCreationDto, ShoppingList>();
        CreateMap<ShoppingListForUpdateDto, ShoppingList>()
            .ReverseMap();
    }
}