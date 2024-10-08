using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request;

public class AssigneesRequest
{
    [Display("Project")]
    [DataSource(typeof(ProjectDataHandler))]
    public string ProjectId { get; set; }
    
    [Display("Assignee IDs"), DataSource(typeof(ProjectMemberDataSourceHandler))]
    public IEnumerable<string>? Assignees { get; set; }
}