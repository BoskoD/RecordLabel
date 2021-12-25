namespace ContentManagement.IntegrationTests.FeatureTests.Artist;

using ContentManagement.SharedTestHelpers.Fakes.Artist;
using ContentManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ContentManagement.Domain.Artists.Features;
using static TestFixture;

public class ArtistQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_artist_with_accurate_props()
    {
        // Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        await InsertAsync(fakeArtistOne);

        // Act
        var query = new GetArtist.ArtistQuery(fakeArtistOne.Id);
        var artists = await SendAsync(query);

        // Assert
        artists.Should().BeEquivalentTo(fakeArtistOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_artist_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetArtist.ArtistQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}