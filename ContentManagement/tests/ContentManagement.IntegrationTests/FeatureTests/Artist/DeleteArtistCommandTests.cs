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

public class DeleteArtistCommandTests : TestBase
{
    [Test]
    public async Task can_delete_artist_from_db()
    {
        // Arrange
        var fakeArtistOne = new FakeArtist { }.Generate();
        await InsertAsync(fakeArtistOne);
        var artist = await ExecuteDbContextAsync(db => db.Artists.SingleOrDefaultAsync());
        var id = artist.Id;

        // Act
        var command = new DeleteArtist.DeleteArtistCommand(id);
        await SendAsync(command);
        var artistResponse = await ExecuteDbContextAsync(db => db.Artists.ToListAsync());

        // Assert
        artistResponse.Count.Should().Be(0);
    }

    [Test]
    public async Task delete_artist_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteArtist.DeleteArtistCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}