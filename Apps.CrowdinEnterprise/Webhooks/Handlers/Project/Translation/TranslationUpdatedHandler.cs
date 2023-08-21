using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Project.Translation;

public class TranslationUpdatedHandler : ProjectWebhookHandler
{
    protected override EventType SubscriptionEvent => EventType.TranslationUpdated;

    public TranslationUpdatedHandler([WebhookParameter] ProjectWebhookInput input) : base(input)
    {
    }
}