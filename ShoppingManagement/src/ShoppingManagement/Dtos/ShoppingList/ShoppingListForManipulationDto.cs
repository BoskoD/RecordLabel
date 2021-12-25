namespace ShoppingManagement.Dtos.ShoppingList;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class ShoppingListForManipulationDto 
{
   public string StoreType { get; set; }
}