namespace ShoppingManagement.FunctionalTests.FunctionalTests.ShoppingList;

using ShoppingManagement.SharedTestHelpers.Fakes.ShoppingList;
using ShoppingManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetShoppingListListTests : TestBase
{
    [Test]
    public async Task get_shoppinglist_list_returns_success()
    {
        // Arrange
        // N/A

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.ShoppingLists.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}