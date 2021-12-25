namespace ContentManagement.IntegrationTests.FeatureTests.Artist;

using ContentManagement.SharedTestHelpers.Fakes.Artist;
using ContentManagement.IntegrationTests.TestUtilities;
using ContentManagement.Dtos.Artist;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ContentManagement.Domain.Artists.Features;
using static TestFixture;

public class UpdateArtistCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_artist_in_db()
    {
        // Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        var updatedArtistDto = new FakeArtistForUpdateDto { }.Generate();
        await InsertAsync(fakeArtistOne);

        var artist = await ExecuteDbContextAsync(db => db.Artists.SingleOrDefaultAsync());
        var id = artist.Id;

        // Act
        var command = new UpdateArtist.UpdateArtistCommand(id, updatedArtistDto);
        await SendAsync(command);
        var updatedArtist = await ExecuteDbContextAsync(db => db.Artists.Where(a => a.Id == id).SingleOrDefaultAsync());

        // Assert
        updatedArtist.Should().BeEquivalentTo(updatedArtistDto, options =>
            options.ExcludingMissingMembers());
    }
}