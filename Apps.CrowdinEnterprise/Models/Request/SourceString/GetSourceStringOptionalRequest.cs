using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Models.Request.SourceString;

public class GetSourceStringOptionalRequest
{
    [Display("String ID")] 
    public string? StringId { get; set; }
}