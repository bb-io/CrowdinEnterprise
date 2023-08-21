using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Project.String;

public class StringAddedHandler : ProjectWebhookHandler
{
    protected override EventType SubscriptionEvent => EventType.StringAdded;

    public StringAddedHandler([WebhookParameter] ProjectWebhookInput input) : base(input, true)
    {
    }
}