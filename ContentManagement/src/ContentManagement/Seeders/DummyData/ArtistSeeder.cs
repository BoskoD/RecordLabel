namespace ContentManagement.Seeders.DummyData;

using AutoBogus;
using ContentManagement.Domain.Artists;
using ContentManagement.Databases;
using System.Linq;

public static class ArtistSeeder
{
    public static void SeedSampleArtistData(ContentDbContext context)
    {
        if (!context.Artists.Any())
        {
            context.Artists.Add(new AutoFaker<Artist>());
            context.Artists.Add(new AutoFaker<Artist>());
            context.Artists.Add(new AutoFaker<Artist>());

            context.SaveChanges();
        }
    }
}