using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Project.Project;

public class ProjectBuiltHandler : ProjectWebhookHandler
{
    protected override EventType SubscriptionEvent => EventType.ProjectBuilt;

    public ProjectBuiltHandler([WebhookParameter(true)] ProjectWebhookInput input) : base(input)
    {
    }
}