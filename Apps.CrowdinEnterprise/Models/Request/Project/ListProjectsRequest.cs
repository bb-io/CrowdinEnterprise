using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.Project;

public class ListProjectsRequest
{
    [Display("User ID")] 
    [DataSource(typeof(UserDataHandler))]
    public string? UserId { get; set; }
    
    [Display("Group ID")] 
    [DataSource(typeof(ProjectGroupDataHandler))]
    public string? GroupID { get; set; }
    
    [Display("Has manager access")] public bool? HasManagerAccess { get; set; }
}