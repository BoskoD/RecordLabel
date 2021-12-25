namespace ShoppingManagement.Domain.ShoppingLists.Validators;

using ShoppingManagement.Dtos.ShoppingList;
using FluentValidation;

public class ShoppingListForUpdateDtoValidator: ShoppingListForManipulationDtoValidator<ShoppingListForUpdateDto>
{
    public ShoppingListForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}