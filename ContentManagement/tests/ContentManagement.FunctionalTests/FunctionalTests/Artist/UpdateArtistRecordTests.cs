namespace ContentManagement.FunctionalTests.FunctionalTests.Artist;

using ContentManagement.SharedTestHelpers.Fakes.Artist;
using ContentManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateArtistRecordTests : TestBase
{
    [Test]
    public async Task put_artist_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeArtist = new FakeArtist { }.Generate();
        var updatedArtistDto = new FakeArtistForUpdateDto { }.Generate();
        await InsertAsync(fakeArtist);

        // Act
        var route = ApiRoutes.Artists.Put.Replace(ApiRoutes.Artists.Id, fakeArtist.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedArtistDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}