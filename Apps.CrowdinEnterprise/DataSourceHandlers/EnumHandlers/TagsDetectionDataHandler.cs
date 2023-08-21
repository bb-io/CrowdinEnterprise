using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;

public class TagsDetectionDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        {"Auto", "Auto"},
        {"Count", "Count"},
        {"Skip", "Skip"},
    };
}