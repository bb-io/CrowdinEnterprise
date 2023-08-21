using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Wrappers;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Response;

public class GroupWebhookResponse : CrowdinWebhookResponse<GroupWrapper>
{
    public GroupEntity Group { get; set; }

    public UserEntity User { get; set; }

    public override void ConfigureResponse(GroupWrapper wrapper)
    {
        Group = new(wrapper.Group);
        User = new(wrapper.User);
    }
}