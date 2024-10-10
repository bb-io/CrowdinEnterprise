using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Models.Request.Suggestions;

public class GetSuggestionOptionalRequest
{
    [Display("Suggestion ID")]
    public string? SuggestionId { get; set; }
}