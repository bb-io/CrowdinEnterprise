using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.Group;

public class ProjectGroupRequest
{
    [Display("Project group")]
    [DataSource(typeof(ProjectGroupDataHandler))]
    public string ProjectGroupId { get; set; }
}