using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;

public class IssueTypeHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData() => new()
    {
        { "GeneralQuestion", "General question" },
        { "TranslationMistake", "Translation mistake" },
        { "ContextRequest", "Context request" },
        { "SourceMistake", "Source mistake" },
    };
}