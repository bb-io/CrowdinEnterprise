using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Project.Project;

public class ProjectApprovedHandler : ProjectWebhookHandler
{
    protected override EventType SubscriptionEvent => EventType.ProjectApproved;

    public ProjectApprovedHandler([WebhookParameter(true)] ProjectWebhookInput input) : base(input)
    {
    }
}