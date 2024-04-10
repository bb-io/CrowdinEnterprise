using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;

public class StringScopeHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData() => new()
    {
        { "Identifier", "Identifier" },
        { "Text", "Text" },
        { "Context", "Context" },
    };
}