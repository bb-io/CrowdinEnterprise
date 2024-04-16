using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;

public class TranslateDuplicatesDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData() => new()
    {
        { "Show", "Show" },
        { "Hide", "Hide" },
        { "ShowAndAutoTranslate", "Show and auto translate" },
        { "ShowWithVersionBranch", "Show with version branch" },
        { "HideStrict", "Hide strict" },
        { "ShowWithVersionBranchStrict", "Show with version branch strict" },
    };
}