using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Models.Request.File;

public class GetFileOptionalRequest
{
    [Display("File ID")]
    public string? FileId { get; set; }
}