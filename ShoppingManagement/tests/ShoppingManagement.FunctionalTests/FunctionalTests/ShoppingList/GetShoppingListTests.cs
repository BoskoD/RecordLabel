namespace ShoppingManagement.FunctionalTests.FunctionalTests.ShoppingList;

using ShoppingManagement.SharedTestHelpers.Fakes.ShoppingList;
using ShoppingManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetShoppingListTests : TestBase
{
    [Test]
    public async Task get_shoppinglist_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeShoppingList = new FakeShoppingList { }.Generate();
        await InsertAsync(fakeShoppingList);

        // Act
        var route = ApiRoutes.ShoppingLists.GetRecord.Replace(ApiRoutes.ShoppingLists.Id, fakeShoppingList.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}