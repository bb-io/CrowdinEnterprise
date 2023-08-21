using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Wrappers;
using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Response;

public class ProjectBuiltWebhookResponse : CrowdinWebhookResponse<ProjectBuildWrapper>
{
    public ProjectEntity Project { get; set; }
    [Display("Build ID")] public string BuildId { get; set; }
    [Display("Build download URL")] public string BuildDownloadUrl { get; set; }

    public override void ConfigureResponse(ProjectBuildWrapper wrapper)
    {
        Project = new(wrapper.Build.Project);
        BuildId = wrapper.Build.Id;
        BuildDownloadUrl = wrapper.Build.DownloadUrl;
    }
}