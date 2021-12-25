namespace ContentManagement.FunctionalTests.FunctionalTests.Artist;

using ContentManagement.SharedTestHelpers.Fakes.Artist;
using ContentManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateArtistTests : TestBase
{
    [Test]
    public async Task create_artist_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeArtist = new FakeArtistForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Artists.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeArtist);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}