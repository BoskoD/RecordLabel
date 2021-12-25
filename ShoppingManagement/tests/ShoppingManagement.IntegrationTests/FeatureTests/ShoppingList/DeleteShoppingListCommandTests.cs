namespace ShoppingManagement.IntegrationTests.FeatureTests.ShoppingList;

using ShoppingManagement.SharedTestHelpers.Fakes.ShoppingList;
using ShoppingManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ShoppingManagement.Domain.ShoppingLists.Features;
using static TestFixture;

public class DeleteShoppingListCommandTests : TestBase
{
    [Test]
    public async Task can_delete_shoppinglist_from_db()
    {
        // Arrange
        var fakeShoppingListOne = new FakeShoppingList { }.Generate();
        await InsertAsync(fakeShoppingListOne);
        var shoppingList = await ExecuteDbContextAsync(db => db.ShoppingLists.SingleOrDefaultAsync());
        var id = shoppingList.Id;

        // Act
        var command = new DeleteShoppingList.DeleteShoppingListCommand(id);
        await SendAsync(command);
        var shoppingListResponse = await ExecuteDbContextAsync(db => db.ShoppingLists.ToListAsync());

        // Assert
        shoppingListResponse.Count.Should().Be(0);
    }

    [Test]
    public async Task delete_shoppinglist_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteShoppingList.DeleteShoppingListCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}