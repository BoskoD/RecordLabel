namespace ShoppingManagement.Domain.ShoppingLists.Validators;

using ShoppingManagement.Dtos.ShoppingList;
using FluentValidation;

public class ShoppingListForCreationDtoValidator: ShoppingListForManipulationDtoValidator<ShoppingListForCreationDto>
{
    public ShoppingListForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}