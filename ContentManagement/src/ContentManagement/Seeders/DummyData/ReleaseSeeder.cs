namespace ContentManagement.Seeders.DummyData;

using AutoBogus;
using ContentManagement.Domain.Releases;
using ContentManagement.Databases;
using System.Linq;

public static class ReleaseSeeder
{
    public static void SeedSampleReleaseData(ContentDbContext context)
    {
        if (!context.Releases.Any())
        {
            context.Releases.Add(new AutoFaker<Release>());
            context.Releases.Add(new AutoFaker<Release>());
            context.Releases.Add(new AutoFaker<Release>());

            context.SaveChanges();
        }
    }
}