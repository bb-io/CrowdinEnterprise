using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Models.Request.Directory;

public class GetDirectoryByPathRequest
{
    public string Path { get; set; }
    
    [Display("Path contains file")]
    public bool? PathContainsFile { get; set; }
}