using Blackbird.Applications.Sdk.Common;
using Crowdin.Api.Translations;

namespace Apps.CrowdinEnterprise.Models.Entities;

public class ProjectBuildEntity
{
    [Display("Build ID")]
    public string Id { get; set; }

    [Display("Project ID")]
    public string ProjectId { get; set; }

    public string Status { get; set; }

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }
    
    public ProjectBuildEntity(ProjectBuild build)
    {
        Id = build.Id.ToString();
        ProjectId = build.ProjectId.ToString();
        Status = build.Status.ToString();
        CreatedAt = build.CreatedAt.DateTime;
    }
}