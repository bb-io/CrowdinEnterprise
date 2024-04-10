using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;

public class TagsDetectionDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData() => new()
    {
        { "Auto", "Auto" },
        { "Count", "Count" },
        { "Skip", "Skip" },
    };
}