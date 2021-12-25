namespace ShoppingManagement.Dtos.ShoppingList;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ShoppingListDto 
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string? LastModifiedBy { get; set; }
   public string StoreType { get; set; }
}