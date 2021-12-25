namespace ShoppingManagement.Seeders.DummyData;

using AutoBogus;
using ShoppingManagement.Domain.ShoppingLists;
using ShoppingManagement.Databases;
using System.Linq;

public static class ShoppingListSeeder
{
    public static void SeedSampleShoppingListData(ShoppingDbContext context)
    {
        if (!context.ShoppingLists.Any())
        {
            context.ShoppingLists.Add(new AutoFaker<ShoppingList>());
            context.ShoppingLists.Add(new AutoFaker<ShoppingList>());
            context.ShoppingLists.Add(new AutoFaker<ShoppingList>());

            context.SaveChanges();
        }
    }
}