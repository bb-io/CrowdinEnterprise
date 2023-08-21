using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;

public class TranslateDuplicatesDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        {"Show", "Show"},
        {"Hide", "Hide"},
        {"ShowAndAutoTranslate", "Show and auto translate"},
        {"ShowWithVersionBranch", "Show with version branch"},
        {"HideStrict", "Hide strict"},
        {"ShowWithVersionBranchStrict", "Show with version branch strict"},
    };
}