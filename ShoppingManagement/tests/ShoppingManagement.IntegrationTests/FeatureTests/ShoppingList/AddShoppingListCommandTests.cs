namespace ShoppingManagement.IntegrationTests.FeatureTests.ShoppingList;

using ShoppingManagement.SharedTestHelpers.Fakes.ShoppingList;
using ShoppingManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ShoppingManagement.Domain.ShoppingLists.Features;
using static TestFixture;
using ShoppingManagement.Exceptions;

public class AddShoppingListCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_shoppinglist_to_db()
    {
        // Arrange
        var fakeShoppingListOne = new FakeShoppingListForCreationDto { }.Generate();

        // Act
        var command = new AddShoppingList.AddShoppingListCommand(fakeShoppingListOne);
        var shoppingListReturned = await SendAsync(command);
        var shoppingListCreated = await ExecuteDbContextAsync(db => db.ShoppingLists.SingleOrDefaultAsync());

        // Assert
        shoppingListReturned.Should().BeEquivalentTo(fakeShoppingListOne, options =>
            options.ExcludingMissingMembers());
        shoppingListCreated.Should().BeEquivalentTo(fakeShoppingListOne, options =>
            options.ExcludingMissingMembers());
    }
}