using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Project.Suggestion;

public class SuggestionApprovedHandler : ProjectWebhookHandler
{
    protected override EventType SubscriptionEvent => EventType.SuggestionApproved;

    public SuggestionApprovedHandler([WebhookParameter(true)] ProjectWebhookInput input) : base(input)
    {
    }
}