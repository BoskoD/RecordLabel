namespace ContentManagement.FunctionalTests.FunctionalTests.Artist;

using ContentManagement.SharedTestHelpers.Fakes.Artist;
using ContentManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetArtistListTests : TestBase
{
    [Test]
    public async Task get_artist_list_returns_success()
    {
        // Arrange
        // N/A

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Artists.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}