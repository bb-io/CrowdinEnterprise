using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Wrappers;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Response;

public class ProjectWithUserWebhookResponse : CrowdinWebhookResponse<ProjectWithUserWrapper>
{
    public ProjectEntity Project { get; set; }

    public UserEntity User { get; set; }

    public override void ConfigureResponse(ProjectWithUserWrapper wrapper)
    {
        Project = new(wrapper.Project);
        User = new(wrapper.User);
    }
}