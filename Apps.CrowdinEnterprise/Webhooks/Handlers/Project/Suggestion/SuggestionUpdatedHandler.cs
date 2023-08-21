using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Project.Suggestion;

public class SuggestionUpdatedHandler : ProjectWebhookHandler
{
    protected override EventType SubscriptionEvent => EventType.SuggestionUpdated;

    public SuggestionUpdatedHandler([WebhookParameter] ProjectWebhookInput input) : base(input)
    {
    }
}