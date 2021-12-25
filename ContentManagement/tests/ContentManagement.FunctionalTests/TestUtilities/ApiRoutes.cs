namespace ContentManagement.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

public static class Artists
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/artists";
        public const string GetRecord = $"{Base}/artists/{Id}";
        public const string Create = $"{Base}/artists";
        public const string Delete = $"{Base}/artists/{Id}";
        public const string Put = $"{Base}/artists/{Id}";
        public const string Patch = $"{Base}/artists/{Id}";
    }

public static class Releases
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/releases";
        public const string GetRecord = $"{Base}/releases/{Id}";
        public const string Create = $"{Base}/releases";
        public const string Delete = $"{Base}/releases/{Id}";
        public const string Put = $"{Base}/releases/{Id}";
        public const string Patch = $"{Base}/releases/{Id}";
    }
}
