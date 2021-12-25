namespace ShoppingManagement.IntegrationTests.FeatureTests.ShoppingList;

using ShoppingManagement.SharedTestHelpers.Fakes.ShoppingList;
using ShoppingManagement.IntegrationTests.TestUtilities;
using ShoppingManagement.Dtos.ShoppingList;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ShoppingManagement.Domain.ShoppingLists.Features;
using static TestFixture;

public class UpdateShoppingListCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_shoppinglist_in_db()
    {
        // Arrange
        var fakeShoppingListOne = new FakeShoppingList { }.Generate();
        var updatedShoppingListDto = new FakeShoppingListForUpdateDto { }.Generate();
        await InsertAsync(fakeShoppingListOne);

        var shoppingList = await ExecuteDbContextAsync(db => db.ShoppingLists.SingleOrDefaultAsync());
        var id = shoppingList.Id;

        // Act
        var command = new UpdateShoppingList.UpdateShoppingListCommand(id, updatedShoppingListDto);
        await SendAsync(command);
        var updatedShoppingList = await ExecuteDbContextAsync(db => db.ShoppingLists.Where(s => s.Id == id).SingleOrDefaultAsync());

        // Assert
        updatedShoppingList.Should().BeEquivalentTo(updatedShoppingListDto, options =>
            options.ExcludingMissingMembers());
    }
}