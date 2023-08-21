using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Project.Suggestion;

public class SuggestionDisapprovedHandler : ProjectWebhookHandler
{
    protected override EventType SubscriptionEvent => EventType.SuggestionDisapproved;

    public SuggestionDisapprovedHandler([WebhookParameter] ProjectWebhookInput input) : base(input)
    {
    }
}