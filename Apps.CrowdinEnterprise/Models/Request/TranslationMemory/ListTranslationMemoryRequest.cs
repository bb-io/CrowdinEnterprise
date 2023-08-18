using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Models.Request.TranslationMemory;

public class ListTranslationMemoryRequest
{
    [Display("User ID")] public string? UserId { get; set; }
    [Display("Group ID")] public string? GroupId { get; set; }
}