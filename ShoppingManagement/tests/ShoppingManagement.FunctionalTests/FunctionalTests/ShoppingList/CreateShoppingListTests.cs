namespace ShoppingManagement.FunctionalTests.FunctionalTests.ShoppingList;

using ShoppingManagement.SharedTestHelpers.Fakes.ShoppingList;
using ShoppingManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateShoppingListTests : TestBase
{
    [Test]
    public async Task create_shoppinglist_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeShoppingList = new FakeShoppingListForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.ShoppingLists.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeShoppingList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}