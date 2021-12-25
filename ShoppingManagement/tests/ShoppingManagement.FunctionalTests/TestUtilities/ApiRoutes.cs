namespace ShoppingManagement.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

public static class ShoppingLists
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/shoppingLists";
        public const string GetRecord = $"{Base}/shoppingLists/{Id}";
        public const string Create = $"{Base}/shoppingLists";
        public const string Delete = $"{Base}/shoppingLists/{Id}";
        public const string Put = $"{Base}/shoppingLists/{Id}";
        public const string Patch = $"{Base}/shoppingLists/{Id}";
    }
}
