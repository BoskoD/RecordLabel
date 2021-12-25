namespace ContentManagement.FunctionalTests.FunctionalTests.Artist;

using ContentManagement.SharedTestHelpers.Fakes.Artist;
using ContentManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteArtistTests : TestBase
{
    [Test]
    public async Task delete_artist_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeArtist = new FakeArtist { }.Generate();
        await InsertAsync(fakeArtist);

        // Act
        var route = ApiRoutes.Artists.Delete.Replace(ApiRoutes.Artists.Id, fakeArtist.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}