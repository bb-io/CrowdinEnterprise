using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Project.String;

public class StringDeletedHandler : ProjectWebhookHandler
{
    protected override EventType SubscriptionEvent => EventType.StringDeleted;

    public StringDeletedHandler([WebhookParameter] ProjectWebhookInput input) : base(input, true)
    {
    }
}