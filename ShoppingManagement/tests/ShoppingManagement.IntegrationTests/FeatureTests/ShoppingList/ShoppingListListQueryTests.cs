namespace ShoppingManagement.IntegrationTests.FeatureTests.ShoppingList;

using ShoppingManagement.Dtos.ShoppingList;
using ShoppingManagement.SharedTestHelpers.Fakes.ShoppingList;
using ShoppingManagement.Exceptions;
using ShoppingManagement.Domain.ShoppingLists.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class ShoppingListListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_shoppinglist_list()
    {
        // Arrange
        var fakeShoppingListOne = new FakeShoppingList { }.Generate();
        var fakeShoppingListTwo = new FakeShoppingList { }.Generate();
        var queryParameters = new ShoppingListParametersDto();

        await InsertAsync(fakeShoppingListOne, fakeShoppingListTwo);

        // Act
        var query = new GetShoppingListList.ShoppingListListQuery(queryParameters);
        var shoppingLists = await SendAsync(query);

        // Assert
        shoppingLists.Should().HaveCount(2);
    }
    
    [Test]
    public async Task can_get_shoppinglist_list_with_expected_page_size_and_number()
    {
        //Arrange
        var fakeShoppingListOne = new FakeShoppingList { }.Generate();
        var fakeShoppingListTwo = new FakeShoppingList { }.Generate();
        var fakeShoppingListThree = new FakeShoppingList { }.Generate();
        var queryParameters = new ShoppingListParametersDto() { PageSize = 1, PageNumber = 2 };

        await InsertAsync(fakeShoppingListOne, fakeShoppingListTwo, fakeShoppingListThree);

        //Act
        var query = new GetShoppingListList.ShoppingListListQuery(queryParameters);
        var shoppingLists = await SendAsync(query);

        // Assert
        shoppingLists.Should().HaveCount(1);
    }
    
    [Test]
    public async Task can_get_sorted_list_of_shoppinglist_by_StoreType_in_asc_order()
    {
        //Arrange
        var fakeShoppingListOne = new FakeShoppingList { }.Generate();
        var fakeShoppingListTwo = new FakeShoppingList { }.Generate();
        fakeShoppingListOne.StoreType = "bravo";
        fakeShoppingListTwo.StoreType = "alpha";
        var queryParameters = new ShoppingListParametersDto() { SortOrder = "StoreType" };

        await InsertAsync(fakeShoppingListOne, fakeShoppingListTwo);

        //Act
        var query = new GetShoppingListList.ShoppingListListQuery(queryParameters);
        var shoppingLists = await SendAsync(query);

        // Assert
        shoppingLists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeShoppingListTwo, options =>
                options.ExcludingMissingMembers());
        shoppingLists
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeShoppingListOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_shoppinglist_by_StoreType_in_desc_order()
    {
        //Arrange
        var fakeShoppingListOne = new FakeShoppingList { }.Generate();
        var fakeShoppingListTwo = new FakeShoppingList { }.Generate();
        fakeShoppingListOne.StoreType = "alpha";
        fakeShoppingListTwo.StoreType = "bravo";
        var queryParameters = new ShoppingListParametersDto() { SortOrder = "-StoreType" };

        await InsertAsync(fakeShoppingListOne, fakeShoppingListTwo);

        //Act
        var query = new GetShoppingListList.ShoppingListListQuery(queryParameters);
        var shoppingLists = await SendAsync(query);

        // Assert
        shoppingLists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeShoppingListTwo, options =>
                options.ExcludingMissingMembers());
        shoppingLists
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeShoppingListOne, options =>
                options.ExcludingMissingMembers());
    }

    
    [Test]
    public async Task can_filter_shoppinglist_list_using_StoreType()
    {
        //Arrange
        var fakeShoppingListOne = new FakeShoppingList { }.Generate();
        var fakeShoppingListTwo = new FakeShoppingList { }.Generate();
        fakeShoppingListOne.StoreType = "alpha";
        fakeShoppingListTwo.StoreType = "bravo";
        var queryParameters = new ShoppingListParametersDto() { Filters = $"StoreType == {fakeShoppingListTwo.StoreType}" };

        await InsertAsync(fakeShoppingListOne, fakeShoppingListTwo);

        //Act
        var query = new GetShoppingListList.ShoppingListListQuery(queryParameters);
        var shoppingLists = await SendAsync(query);

        // Assert
        shoppingLists.Should().HaveCount(1);
        shoppingLists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeShoppingListTwo, options =>
                options.ExcludingMissingMembers());
    }

}