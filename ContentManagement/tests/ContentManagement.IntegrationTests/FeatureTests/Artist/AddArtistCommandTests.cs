namespace ContentManagement.IntegrationTests.FeatureTests.Artist;

using ContentManagement.SharedTestHelpers.Fakes.Artist;
using ContentManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ContentManagement.Domain.Artists.Features;
using static TestFixture;
using ContentManagement.Exceptions;

public class AddArtistCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_artist_to_db()
    {
        // Arrange
        var fakeArtistOne = new FakeArtistForCreationDto { }.Generate();

        // Act
        var command = new AddArtist.AddArtistCommand(fakeArtistOne);
        var artistReturned = await SendAsync(command);
        var artistCreated = await ExecuteDbContextAsync(db => db.Artists.SingleOrDefaultAsync());

        // Assert
        artistReturned.Should().BeEquivalentTo(fakeArtistOne, options =>
            options.ExcludingMissingMembers());
        artistCreated.Should().BeEquivalentTo(fakeArtistOne, options =>
            options.ExcludingMissingMembers());
    }
}