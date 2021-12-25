namespace ShoppingManagement.Domain.ShoppingLists;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class ShoppingList : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public string StoreType { get; set; }
}