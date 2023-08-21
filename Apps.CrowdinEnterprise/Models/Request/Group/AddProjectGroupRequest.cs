using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.Group;

public class AddProjectGroupRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    
    [Display("Parent group ID")]
    [DataSource(typeof(ProjectGroupDataHandler))]
    public string ParentId { get; set; }
}