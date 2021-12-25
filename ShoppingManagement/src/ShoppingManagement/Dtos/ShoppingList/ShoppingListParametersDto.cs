namespace ShoppingManagement.Dtos.ShoppingList;

using ShoppingManagement.Dtos.Shared;

public class ShoppingListParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}