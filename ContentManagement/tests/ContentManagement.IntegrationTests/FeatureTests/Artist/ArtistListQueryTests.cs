namespace ContentManagement.IntegrationTests.FeatureTests.Artist;

using ContentManagement.Dtos.Artist;
using ContentManagement.SharedTestHelpers.Fakes.Artist;
using ContentManagement.Exceptions;
using ContentManagement.Domain.Artists.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class ArtistListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_artist_list()
    {
        // Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        var queryParameters = new ArtistParametersDto();

        await InsertAsync(fakeArtistOne, fakeArtistTwo);

        // Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists.Should().HaveCount(2);
    }
    
    [Test]
    public async Task can_get_artist_list_with_expected_page_size_and_number()
    {
        //Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        var fakeArtistThree = new FakeArtist { }.Generate();
        var queryParameters = new ArtistParametersDto() { PageSize = 1, PageNumber = 2 };

        await InsertAsync(fakeArtistOne, fakeArtistTwo, fakeArtistThree);

        //Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists.Should().HaveCount(1);
    }
    
    [Test]
    public async Task can_get_sorted_list_of_artist_by_ArtistId_in_asc_order()
    {
        //Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        fakeArtistOne.ArtistId = 2;
        fakeArtistTwo.ArtistId = 1;
        var queryParameters = new ArtistParametersDto() { SortOrder = "ArtistId" };

        await InsertAsync(fakeArtistOne, fakeArtistTwo);

        //Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistTwo, options =>
                options.ExcludingMissingMembers());
        artists
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_artist_by_ArtistId_in_desc_order()
    {
        //Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        fakeArtistOne.ArtistId = 1;
        fakeArtistTwo.ArtistId = 2;
        var queryParameters = new ArtistParametersDto() { SortOrder = "-ArtistId" };

        await InsertAsync(fakeArtistOne, fakeArtistTwo);

        //Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistTwo, options =>
                options.ExcludingMissingMembers());
        artists
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_artist_by_ArtistName_in_asc_order()
    {
        //Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        fakeArtistOne.ArtistName = "bravo";
        fakeArtistTwo.ArtistName = "alpha";
        var queryParameters = new ArtistParametersDto() { SortOrder = "ArtistName" };

        await InsertAsync(fakeArtistOne, fakeArtistTwo);

        //Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistTwo, options =>
                options.ExcludingMissingMembers());
        artists
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_artist_by_ArtistName_in_desc_order()
    {
        //Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        fakeArtistOne.ArtistName = "alpha";
        fakeArtistTwo.ArtistName = "bravo";
        var queryParameters = new ArtistParametersDto() { SortOrder = "-ArtistName" };

        await InsertAsync(fakeArtistOne, fakeArtistTwo);

        //Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistTwo, options =>
                options.ExcludingMissingMembers());
        artists
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_artist_by_Country_in_asc_order()
    {
        //Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        fakeArtistOne.Country = "bravo";
        fakeArtistTwo.Country = "alpha";
        var queryParameters = new ArtistParametersDto() { SortOrder = "Country" };

        await InsertAsync(fakeArtistOne, fakeArtistTwo);

        //Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistTwo, options =>
                options.ExcludingMissingMembers());
        artists
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_artist_by_Country_in_desc_order()
    {
        //Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        fakeArtistOne.Country = "alpha";
        fakeArtistTwo.Country = "bravo";
        var queryParameters = new ArtistParametersDto() { SortOrder = "-Country" };

        await InsertAsync(fakeArtistOne, fakeArtistTwo);

        //Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistTwo, options =>
                options.ExcludingMissingMembers());
        artists
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_artist_by_Biography_in_asc_order()
    {
        //Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        fakeArtistOne.Biography = "bravo";
        fakeArtistTwo.Biography = "alpha";
        var queryParameters = new ArtistParametersDto() { SortOrder = "Biography" };

        await InsertAsync(fakeArtistOne, fakeArtistTwo);

        //Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistTwo, options =>
                options.ExcludingMissingMembers());
        artists
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_artist_by_Biography_in_desc_order()
    {
        //Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        fakeArtistOne.Biography = "alpha";
        fakeArtistTwo.Biography = "bravo";
        var queryParameters = new ArtistParametersDto() { SortOrder = "-Biography" };

        await InsertAsync(fakeArtistOne, fakeArtistTwo);

        //Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistTwo, options =>
                options.ExcludingMissingMembers());
        artists
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistOne, options =>
                options.ExcludingMissingMembers());
    }

    
    [Test]
    public async Task can_filter_artist_list_using_ArtistId()
    {
        //Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        fakeArtistOne.ArtistId = 1;
        fakeArtistTwo.ArtistId = 2;
        var queryParameters = new ArtistParametersDto() { Filters = $"ArtistId == {fakeArtistTwo.ArtistId}" };

        await InsertAsync(fakeArtistOne, fakeArtistTwo);

        //Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists.Should().HaveCount(1);
        artists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_artist_list_using_ArtistName()
    {
        //Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        fakeArtistOne.ArtistName = "alpha";
        fakeArtistTwo.ArtistName = "bravo";
        var queryParameters = new ArtistParametersDto() { Filters = $"ArtistName == {fakeArtistTwo.ArtistName}" };

        await InsertAsync(fakeArtistOne, fakeArtistTwo);

        //Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists.Should().HaveCount(1);
        artists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_artist_list_using_Country()
    {
        //Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        fakeArtistOne.Country = "alpha";
        fakeArtistTwo.Country = "bravo";
        var queryParameters = new ArtistParametersDto() { Filters = $"Country == {fakeArtistTwo.Country}" };

        await InsertAsync(fakeArtistOne, fakeArtistTwo);

        //Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists.Should().HaveCount(1);
        artists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_artist_list_using_Biography()
    {
        //Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var fakeArtistTwo = new FakeArtist { }.Generate();
        fakeArtistOne.Biography = "alpha";
        fakeArtistTwo.Biography = "bravo";
        var queryParameters = new ArtistParametersDto() { Filters = $"Biography == {fakeArtistTwo.Biography}" };

        await InsertAsync(fakeArtistOne, fakeArtistTwo);

        //Act
        var query = new GetArtistList.ArtistListQuery(queryParameters);
        var artists = await SendAsync(query);

        // Assert
        artists.Should().HaveCount(1);
        artists
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArtistTwo, options =>
                options.ExcludingMissingMembers());
    }

}