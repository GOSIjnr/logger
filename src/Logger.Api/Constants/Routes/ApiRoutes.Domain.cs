namespace Logger.Api.Constants.Routes;

internal static partial class ApiRoutes
{
    public static class Organizations
    {
        public const string Base = $"{ApiBasePath}/organizations";
        public const string Create = "";
        public const string List = "";
        public const string GetById = "{id:guid}";
    }

    public static class Sheets
    {
        public const string Base = $"{ApiBasePath}/sheets";
        public const string Create = "";
        public const string ListByOrg = "organization/{orgId:guid}";
    }

    public static class Incidents
    {
        public const string Base = $"{ApiBasePath}/incidents";
        public const string Create = "";
        public const string ListBySheet = "sheet/{sheetId:guid}";
        public const string Update = "{id:guid}";
    }
}
