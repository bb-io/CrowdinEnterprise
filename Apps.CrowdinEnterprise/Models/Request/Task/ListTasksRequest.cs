using Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;
using Apps.CrowdinEnterprise.Models.Request.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.Task;

public class ListTasksRequest : ProjectRequest
{
    [DataSource(typeof(TaskStatusHandler))]
    public string? Status { get; set; }
    
    [Display("Assignee ID")]
    public string? AssigneeId { get; set; }
}