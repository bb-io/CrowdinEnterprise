using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Models.Request.Task;

public class GetTaskOptionalRequest
{
    [Display("Task ID")]
    public string? TaskId { get; set; }
}