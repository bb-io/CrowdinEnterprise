using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;

public class IssueStatusHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData() => new()
    {
        { "Resolved", "Resolved" },
        { "Unresolved", "Unresolved" },
    };
}