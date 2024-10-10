using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.Project;

public class GetProjectOptionalRequest
{
    [Display("Project")]
    [DataSource(typeof(ProjectDataHandler))]
    public string? ProjectId { get; set; }
}