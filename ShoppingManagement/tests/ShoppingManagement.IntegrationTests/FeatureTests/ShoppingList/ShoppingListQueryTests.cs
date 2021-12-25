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

public class ShoppingListQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_shoppinglist_with_accurate_props()
    {
        // Arrange
        var fakeShoppingListOne = new FakeShoppingList { }.Generate();
        await InsertAsync(fakeShoppingListOne);

        // Act
        var query = new GetShoppingList.ShoppingListQuery(fakeShoppingListOne.Id);
        var shoppingLists = await SendAsync(query);

        // Assert
        shoppingLists.Should().BeEquivalentTo(fakeShoppingListOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_shoppinglist_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetShoppingList.ShoppingListQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}