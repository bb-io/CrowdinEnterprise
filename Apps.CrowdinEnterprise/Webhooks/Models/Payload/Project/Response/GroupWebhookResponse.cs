using Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Wrappers;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Response;

public class GroupWebhookResponse : CrowdinWebhookResponse<GroupWrapper>
{


    public override void ConfigureResponse(GroupWrapper wrapper)
    {
    }
}