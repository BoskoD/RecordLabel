namespace ContentManagement.FunctionalTests.FunctionalTests.Artist;

using ContentManagement.SharedTestHelpers.Fakes.Artist;
using ContentManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetArtistTests : TestBase
{
    [Test]
    public async Task get_artist_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeArtist = new FakeArtist { }.Generate();
        await InsertAsync(fakeArtist);

        // Act
        var route = ApiRoutes.Artists.GetRecord.Replace(ApiRoutes.Artists.Id, fakeArtist.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}