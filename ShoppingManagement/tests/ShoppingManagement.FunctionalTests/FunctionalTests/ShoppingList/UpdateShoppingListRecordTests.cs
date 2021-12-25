namespace ShoppingManagement.FunctionalTests.FunctionalTests.ShoppingList;

using ShoppingManagement.SharedTestHelpers.Fakes.ShoppingList;
using ShoppingManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateShoppingListRecordTests : TestBase
{
    [Test]
    public async Task put_shoppinglist_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeShoppingList = new FakeShoppingList { }.Generate();
        var updatedShoppingListDto = new FakeShoppingListForUpdateDto { }.Generate();
        await InsertAsync(fakeShoppingList);

        // Act
        var route = ApiRoutes.ShoppingLists.Put.Replace(ApiRoutes.ShoppingLists.Id, fakeShoppingList.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedShoppingListDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}