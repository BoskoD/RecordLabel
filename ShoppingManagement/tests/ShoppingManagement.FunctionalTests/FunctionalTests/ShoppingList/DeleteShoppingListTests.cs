namespace ShoppingManagement.FunctionalTests.FunctionalTests.ShoppingList;

using ShoppingManagement.SharedTestHelpers.Fakes.ShoppingList;
using ShoppingManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteShoppingListTests : TestBase
{
    [Test]
    public async Task delete_shoppinglist_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeShoppingList = new FakeShoppingList { }.Generate();
        await InsertAsync(fakeShoppingList);

        // Act
        var route = ApiRoutes.ShoppingLists.Delete.Replace(ApiRoutes.ShoppingLists.Id, fakeShoppingList.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}