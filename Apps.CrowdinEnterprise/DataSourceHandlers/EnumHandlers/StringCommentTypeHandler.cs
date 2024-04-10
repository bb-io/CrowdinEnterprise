using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;

public class StringCommentTypeHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData() => new()
    {
        { "Comment", "Comment" },
        { "Issue", "Issue" },
    };
}