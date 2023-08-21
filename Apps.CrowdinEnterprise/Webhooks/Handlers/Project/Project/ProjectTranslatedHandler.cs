using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Project.Project;

public class ProjectTranslatedHandler : ProjectWebhookHandler
{
    protected override EventType SubscriptionEvent => EventType.ProjectTranslated;

    public ProjectTranslatedHandler([WebhookParameter] ProjectWebhookInput input) : base(input)
    {
    }
}